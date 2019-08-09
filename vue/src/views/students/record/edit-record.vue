<template>
  <div>
    <Modal :title="L('EditRecord')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" >
      <Form ref="recordForm" label-position="top" :rules="RecordRule" :model="record">
        <FormItem :label="L('RecordCategory')" prop="category">
          <Input v-model="record.category"/>
        </FormItem>
        <FormItem :label="L('RecordContent')" prop="content">
          <Input v-model="record.content" type="textarea" :rows="3"/>
        </FormItem>
        <FormItem :label="L('RecordProgress')" prop="progress">
          <Input v-model="record.progress"/>
        </FormItem>
        <FormItem :label="L('RecordDate')" prop="date">
          <DatePicker type="date" placeholder="Select date" v-model="record.date"></DatePicker>
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
import Record from "../../../store/entities/record";
@Component
export default class EditRecorde extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  record: Record = new Record();
  save() {
    (this.$refs.recordForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "record/update",
          data: this.record
        });
        (this.$refs.recordForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.recordForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.record = Util.extend(true, {}, this.$store.state.record.editRecord);
    }
  }
  RecordRule = {
    content: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("RecordContent")),
        trigger: "blur"
      }
    ]
  };
}
</script>

