<template>
  <div>
    <Modal :title="L('CreateNewSubject')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="subjectForm" label-position="top" :rules="SubjectRule" :model="subject">
        <FormItem :label="L('SubjectName')" prop="name">
          <Input v-model="subject.name" />
        </FormItem>
        <FormItem :label="L('SortOrder')" prop="order">
          <Input v-model="subject.order" type="number" />
        </FormItem>
        <FormItem>
          <Checkbox v-model="subject.isActive">{{L('IsActive')}}</Checkbox>
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
import Subject from "../../../store/entities/subject";
@Component
export default class CreateSubject extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  subject: Subject = new Subject();
  save() {
    (this.$refs.subjectForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "subject/create",
          data: this.subject
        });
        (this.$refs.subjectForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.subjectForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
    }
  }
  SubjectRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("SubjectName")),
        trigger: "blur"
      }
    ],
    order: [
      {
        type: "number",
        required: false,
        message: this.L("FieldMustBeANumber", undefined, this.L("SordOrder")),
        trigger: "blur",
        transform(value) {
          if (!value) {
            return 0;
          }
          if (isNaN(value)) {
            return value;
          }
          return Number(value);
        }
      }
    ]
  };
}
</script>

