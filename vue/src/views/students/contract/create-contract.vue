<template>
  <div>
    <Modal
      :title="L('CreateNewContract')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
    >
      <Form ref="contractForm" label-position="top" :rules="ContractRule" :model="contract">
        <Row :gutter="16">
          <Col span="12">
            <FormItem :label="L('OrderDate')" prop="orderDate">
              <DatePicker type="date" placeholder="Select date" v-model="order.orderDate"></DatePicker>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem :label="L('SchoolBegin')" prop="schoolBegin">
              <DatePicker type="date" placeholder="Select date" v-model="order.schoolBegin"></DatePicker>
            </FormItem>
          </Col>
        </Row>

        <FormItem :label="L('ContractCategory')" prop="category">
          <Input v-model="contract.category" />
        </FormItem>
        <FormItem :label="L('ContractContent')" prop="content">
          <Input v-model="contract.content" type="textarea" :rows="3" />
        </FormItem>
        <FormItem :label="L('ContractProgress')" prop="progress">
          <Input v-model="contract.progress" />
        </FormItem>
        <FormItem :label="L('ContractDate')" prop="date">
          <DatePicker type="date" placeholder="Select date" v-model="contract.date"></DatePicker>
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
import Contract from "../../../store/entities/contract";
@Component
export default class CreateContract extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  contract: Contract = new Contract();
  student: Student = new Student();
  save() {
    (this.$refs.contractForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.contract.studentId = this.student.id;
        await this.$store.dispatch({
          type: "contract/create",
          data: this.contract
        });
        (this.$refs.contractForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.contractForm as any).resetFields();
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
  ContractRule = {
    content: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("ContractContent")),
        trigger: "blur"
      }
    ]
  };
}
</script>

