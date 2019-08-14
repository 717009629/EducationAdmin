<template>
  <div>
    <Modal :title="L('EditContract')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" width="1000px">
      <div>
        <Form ref="orderForm" label-position="top" :model="order">
          <Row :gutter="16">
            <i-col span=8>
              <FormItem :label="L('CourseCategory')" prop="courseId">
                <Input :value="order.course?order.course.category:''" disabled />
              </FormItem>
            </i-col>
            <i-col span=8>
              <FormItem :label="L('CourseBame')" prop="courseId">
                <Input :value="order.course?order.course.name:''" disabled />
              </FormItem>
            </i-col>
            <i-col span=8>
              <FormItem :label="L('Price')" prop="courseId">
                <Input :value="order.course?order.course.price:''" disabled />
              </FormItem>
            </i-col>
          </Row>
          <Row :gutter="16">
            <i-col span="8">
              <FormItem :label="L('OrderDate')" prop="orderDate">
                <DatePicker type="date" placeholder="Select date" v-model="order.orderDate" disabled></DatePicker>
              </FormItem>
            </i-col>
            <i-col span="8">
              <FormItem :label="L('SchoolBegin')" prop="schoolBegin">
                <DatePicker type="date" placeholder="Select date" v-model="order.schoolBegin" disabled></DatePicker>
              </FormItem>
            </i-col>
            <i-col span='8'>
              <FormItem :label="L('OrderState')" prop="state">
                <Input v-model="order.state" disabled />
              </FormItem>
            </i-col>
          </Row>
          <FormItem :label="L('Note')" prop="note">
            <Input v-model="order.note" disabled />
          </FormItem>
        </Form>
      </div>
      <hr style="margin:20px 0" />
      <Form ref="contractForm" label-position="top" :rules="ContractRule" :model="contract">
        <Row :gutter="16">
          <i-col span="8">
            <FormItem :label="L('StartDate')" prop="startDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.startDate"></DatePicker>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem :label="L('EndDate')" prop="endDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.endDate"></DatePicker>
            </FormItem>
          </i-col>
          <i-col span="8">
            <FormItem :label="L('FullMoney')" prop="fullMoney">
              <InputNumber v-model="contract.fullMoney" style="width:100%"></InputNumber>
            </FormItem>
          </i-col>
        </Row>

        <FormItem :label="L('Note')" prop="note">
          <Input v-model="contract.note" />
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
import Order from "../../../store/entities/order";

@Component
export default class EditContracte extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  contract: Contract = new Contract();
  order: Order = new Order();
  get list() {
    return this.$store.state.courseItem.list;
  }
  get loading() {
    return this.$store.state.courseItem.loading;
  }

  save() {
    (this.$refs.contractForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "contract/update",
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
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.contract = Util.extend(
        true,
        {},
        this.$store.state.contract.editContract
      );
      this.order=this.contract.order;
    }
  }
  ContractRule = {
    startDate: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("StartDate")),
        trigger: "blur"
      }
    ],
    endDate: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("EndDate")),
        trigger: "blur"
      }
    ]
  };
  columns = [
    // {
    //   title: this.L("OrderName"),
    //   key: "name"
    // },
    {
      title: this.L("CourseCategory"),
      key: "courseCategory"
    },
    {
      title: this.L("CourseName"),
      key: "courseName"
    },
    {
      title: this.L("Count"),
      key: "count"
    },
    {
      title: this.L("FullMoney"),
      key: "fullMoney"
    },
    {
      title: this.L("Note"),
      key: "note"
    }
  ];
}
</script>

