<template>
  <div>
    <Modal :title="L('CreateNewOrder')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="orderForm" label-position="top" :rules="OrderRule" :model="order">

        <FormItem :label="L('Course')" prop="courseId">
          <Select v-model="order.courseId" filterable>
            <Option v-for="item in courseList" :value="item.id" :key="item.id" :label="item.category+'：'+item.name+'：'+item.price+'元'">
              <span>{{item.category}}</span>
              <span>：</span>
              <span>{{item.name}}</span>
              <span style="float:right;">{{item.price}}元</span>
            </Option>
          </Select>
        </FormItem>

        <Row :gutter="16">
          <i-col span="12">
            <FormItem :label="L('OrderDate')" prop="orderDate">
              <DatePicker type="date" placeholder="Select date" v-model="order.orderDate"></DatePicker>
            </FormItem>
          </i-col>
          <i-col span="12">
            <FormItem :label="L('SchoolBegin')" prop="schoolBegin">
              <DatePicker type="date" placeholder="Select date" v-model="order.schoolBegin"></DatePicker>
            </FormItem>
          </i-col>
        </Row>

        <!-- <FormItem :label="L('FullMoney')" prop="fullMoney">
          <Input v-model="order.fullMoney" />
        </FormItem> -->
        <FormItem :label="L('OrderState')" prop="state">
          <Input v-model="order.state" />
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
export default class CreateOrder extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  order: Order = new Order();
  student: Student = new Student();
  get courseList() {
    return this.$store.state.course.list;
  }

  save() {
    (this.$refs.orderForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.order.studentId = this.student.id;
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

  async getAllCourse() {
    await this.$store.dispatch({
      type: "course/getAll"
    });
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
      this.getAllCourse()
    }
  }
  OrderRule = {
    orderDate: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("OrderDate")),
        trigger: "blur"
      }
    ],
    schoolBegin: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("SchoolBegin")),
        trigger: "blur"
      }
    ]
  };
}
</script>

