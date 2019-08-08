<template>
  <div>
    <Modal
      :title="L('CreateNewOrder')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      :transfer="false"
    >
      <Form ref="orderForm" label-position="top" :rules="OrderRule" :model="order">
        <FormItem :label="L('OrderCategory')" prop="name">
          <Input v-model="order.category"/>
        </FormItem>
        <FormItem :label="L('OrderContent')" prop="name">
          <Input v-model="order.content" type="textarea" :rows="3"/>
        </FormItem>
        <FormItem :label="L('OrderProgress')" prop="name">
          <Input v-model="order.progress"/>
        </FormItem>
        <FormItem :label="L('OrderDate')" prop="name">
           <DatePicker type="date" placeholder="Select date" v-model="order.date"></DatePicker>
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
import Order from "../../../store/entities/order";
@Component
export default class CreateOrder extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  order: Order = new Order();
  student: Student = new Student();
  save() {
    (this.$refs.orderForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.order.studentId=this.student.id;
        await this.$store.dispatch({
          type: "order/create",
          data: this.order
        });
        (this.$refs.orderForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.orderForm as any).resetFields();
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
  OrderRule = {
    content: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("OrderContent")),
        trigger: "blur"
      }
    ]
  };
}
</script>

