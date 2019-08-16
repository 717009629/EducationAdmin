<template>
  <div>
    <Modal :title="L('EditContract')" :value="value"  @on-visible-change="visibleChange" :mask-closable="false" width="1000px">
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
      <Form ref="contractForm" label-position="top" :model="contract">
        <Row :gutter="16">
          <i-col span="8">
          <FormItem :label="L('StartDate')" prop="startDate">
            <DatePicker type="date" placeholder="Select date" disabled v-model="contract.startDate"></DatePicker>
          </FormItem>
          </i-col>
          <i-col span="8">
          <FormItem :label="L('EndDate')" prop="endDate">
            <DatePicker type="date" placeholder="Select date" disabled v-model="contract.endDate"></DatePicker>
          </FormItem>
          </i-col>
          <i-col span="8">
          <FormItem :label="L('FullMoney')" prop="fullMoney">
            <InputNumber v-model="contract.fullMoney" disabled style="width:100%"/>
          </FormItem>
          </i-col>
        </Row>

        <FormItem :label="L('Note')" prop="note">
          <Input v-model="contract.note" disabled />
        </FormItem>
      </Form>

      <hr style="margin:20px 0" />

      <Form ref="auditeForm" label-position="top" :rules="AuditeRule" :model="audite">

        <FormItem :label="L('Reason For Action')" prop="reason">
          <Input v-model="audite.reason"  />
        </FormItem>
      </Form>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="obsolete" type="error">{{L('Obsolete')}}</Button>
        <Button @click="reject" type="warning">{{L('Reject')}}</Button>
        <Button @click="pass" type="primary">{{L('Pass')}}</Button>
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
import Audite from '../../../store/entities/audite'
import ContractState from '../../../store/entities/contractState'
import Order from '../../../store/entities/order'

@Component
export default class AuditeContracte extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  contract: Contract = new Contract();
  audite:Audite=new Audite();
  order:Order=new Order();

  get list() {
    return this.$store.state.courseItem.list;
  }
  get loading() {
    return this.$store.state.courseItem.loading;
  }


  pass() {
    (this.$refs.auditeForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.audite.contractId=this.contract.id
        this.audite.contractState= ContractState.Audited
        await this.$store.dispatch({
          type: "contract/audite",
          data: this.audite
        });
        (this.$refs.auditeForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  reject() {
    (this.$refs.auditeForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.audite.contractId=this.contract.id
        this.audite.contractState= ContractState.Reject
        await this.$store.dispatch({
          type: "contract/audite",
          data: this.audite
        });
        (this.$refs.auditeForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }

    obsolete() {
    (this.$refs.auditeForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.audite.contractId=this.contract.id
        this.audite.contractState= ContractState.Obsolete
        await this.$store.dispatch({
          type: "contract/audite",
          data: this.audite
        });
        (this.$refs.auditeForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }


  cancel() {
    (this.$refs.auditeForm as any).resetFields();
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
  AuditeRule = {
    reason: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Reason")),
        trigger: "blur"
      }
    ],
    
  };
}
</script>

