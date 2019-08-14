<template>
  <div>
    <Modal :title="L('CreateNewRecord')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="recordForm" label-position="top" :rules="RecordRule" :model="record">

        <FormItem :label="L('RecordContent')" prop="content">
          <Input v-model="record.content" type="textarea" :rows="3" />
        </FormItem>
        <FormItem :label="L('Progress')" prop="state">
          <Select v-model="record.state">
            <Option v-for="n in 3" :key="n" :value="n">{{L(CustomerState[n] )}}</Option>
          </Select>
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
import Customer from "../../../store/entities/customer";
import CustomerState from "../../../store/entities/customerState";
import Record from "../../../store/entities/record";
@Component
export default class CreateRecord extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  record: Record = new Record();
  customer: Customer = new Customer();
  CustomerState=CustomerState;
 
  save() {
    (this.$refs.recordForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.record.customerId = this.customer.id;
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
      this.customer = Util.extend(
        true,
        {},
        this.$store.state.customer.editCustomer
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
    ],
    state: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("RecordProgress")),
        trigger: "blur"
      }
    ],
    date: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("RecordDate")),
        trigger: "blur"
      }
    ]
  };
}
</script>

