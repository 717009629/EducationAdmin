<template>
  <div>
    <Modal :title="L('EditOrder')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="orderForm" label-position="top" :rules="OrderRule" :model="order">

        <FormItem :label="L('Course')" prop="courseId">
          <Select v-model="order.courseId" filterable @on-change='selectChange'>
            <Option v-for="item in courseList" :value="item.id" :key="item.id" :label="item.category+'：'+item.name+'：'+item.count+'节课：'+item.price+'元'">
              <span>{{item.category}}</span>
              <span>：</span>
              <span>{{item.name}}</span>
              <span>：</span>
              <span>{{item.count}}</span>
              <span>节课</span>
              <span style="float:right;">{{item.price}}元</span>
            </Option>
          </Select>
        </FormItem>

        <Row :gutter="16">
          <i-col span="12">
            <FormItem :label="L('FullMoney')" prop="fullMoney">
              <InputNumber v-model="order.fullMoney" style="width:100%" />
            </FormItem>
          </i-col>
          <i-col span="12">
            <FormItem :label="L('LessonCount')" prop="count">
              <InputNumber v-model="order.count" style="width:100%" />
            </FormItem>
          </i-col>
        </Row>
        <FormItem :label="L('OrderDate')" prop="orderDate">
          <DatePicker type="date" placeholder="Select date" v-model="order.orderDate"></DatePicker>
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
  get courseList() {
    return this.$store.state.course.list;
  }
  selectChange(option) {
    let courses = this.courseList.filter(m => m.id === option);
    if (courses.length === 0) {
      this.order.fullMoney = 0;
      return;
    }
    this.order.fullMoney = courses[0].price;
  }
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

  async getAllCourse() {
    await this.$store.dispatch({
      type: "course/getAll"
    });
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.order = Util.extend(true, {}, this.$store.state.order.editOrder);
      this.getAllCourse();
    }
  }
  OrderRule = {
    courseId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("CoureId")),
        trigger: "blur"
      }
    ],
    fullMoney: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("FullMoney")),
        trigger: "blur"
      }
    ],
    orderDate: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("OrderDate")),
        trigger: "blur"
      }
    ]
    // schoolBegin: [
    //   {
    //     type: "date",
    //     required: true,
    //     message: this.L("FieldIsRequired", undefined, this.L("SchoolBegin")),
    //     trigger: "blur"
    //   }
    // ]
  };
}
</script>

