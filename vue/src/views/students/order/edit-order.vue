<template>
  <div>
    <Modal :title="L('EditOrder')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" class="modal-second">
      <Form ref="orderForm" label-position="top" :rules="OrderRule" :model="order">
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

        <FormItem :label="L('FullMoney')" prop="fullMoney">
          <Input v-model="order.fullMoney" />
        </FormItem>
        <FormItem :label="L('OrderState')" prop="state">
          <Input v-model="order.state" />
        </FormItem>
        <FormItem :label="L('ClassName')" prop="className">
          <Input v-model="order.className" />
        </FormItem>
        <FormItem :label="L('Note')" prop="note">
          <Input v-model="order.note" type="textarea" :rows="3" />
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
export default class EditOrdere extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  order: Order = new Order();
  save() {
    (this.$refs.orderForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "order/update",
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
      this.order = Util.extend(true, {}, this.$store.state.order.editOrder);
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

