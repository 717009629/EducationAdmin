<template>
  <div>
    <Modal :title="L('EditSubject')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="subjectForm" label-position="top" :rules="SubjectRule" :model="subject">
        <FormItem :label="L('SubjectName')" prop="name">
          <Input v-model="subject.name" />
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
import Subject from "../../../store/entities/subject";
@Component
export default class EditSubjecte extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  subject: Subject = new Subject();
  save() {
    (this.$refs.subjectForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "subject/update",
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
      this.subject = Util.extend(
        true,
        {},
        this.$store.state.subject.editSubject
      );
    }
  }
  SubjectRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("SubjectName")),
        trigger: "blur"
      }
    ]
  };
}
</script>

