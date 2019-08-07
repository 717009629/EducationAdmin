<template>
  <div>
    <Modal
      :title="L('CreateNewRecord')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      :transfer="false"
    >
      <Form ref="recordForm" label-position="top" :rules="RecordRule" :model="record">
        <FormItem :label="L('RecordCategory')" prop="name">
          <Input v-model="record.category"></Input>
        </FormItem>
        <FormItem :label="L('RecordContent')" prop="name">
          <Input v-model="record.content" type="textarea" :rows="3"></Input>
        </FormItem>
        <FormItem :label="L('RecordProgress')" prop="name">
          <Input v-model="record.progress"></Input>
        </FormItem>
        <FormItem :label="L('RecordDate')" prop="name">
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
export default class CreateRecord extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  record: Record = new Record();
  student: Student = new Student();
  save() {
    (this.$refs.recordForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.record.studentId=this.student.id;
        await this.$store.dispatch({
          type: "record/create",
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
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
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

