<template>
  <div>
    <Modal :title="L('CreateNewOption')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="optionForm" label-position="top" :rules="OptionRule" :model="option">
        <FormItem :label="L('Category')" prop="category">
          <Select v-model="option.category" disabled>
            <Option v-for="item in categories" :value="item.value" :key="item.value">{{L(item.name)}}</Option>
          </Select>
        </FormItem>
        <FormItem :label="L('OptionName')" prop="name">
          <Input v-model="option.name" />
        </FormItem>
        <FormItem>
          <Checkbox v-model="option.isActive">{{L('IsActive')}}</Checkbox>
        </FormItem>
      </Form>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="save" type="primary">{{L('OK')}}</Button>
      </div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Option from "../../../store/entities/option";
import Categories from "../../../store/entities/optionCategory";
@Component
export default class CreateOption extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  @Prop({ type: String }) category: string;
  option: Option = new Option();
  categories = Categories;
  save() {
    (this.$refs.optionForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "option/create",
          data: this.option
        });
        (this.$refs.optionForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  @Watch("category")
  cateoryChange(n, o) {
    this.option.category = n;
  }
  cancel() {
    (this.$refs.optionForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      //  this.option.category = this.category;
    }
  }
  created() {
    this.option.category = this.category;
  }
  OptionRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("OptionName")),
        trigger: "blur"
      }
    ],
    category: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Category")),
        trigger: "blur"
      }
    ]
  };
}
</script>

