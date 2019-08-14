<template>
  <div>
    <Modal :title="L('CreateNewCustomer')" :value="value" @on-ok="save" @on-visible-change="visibleChange">
      <Form ref="customerForm" label-position="top" :rules="customerRule" :model="customer">

        <FormItem :label="L('StudentName')" prop="studentName">
          <Input v-model="customer.studentName" />
        </FormItem>


        <Row :gutter="16">
          <i-col span="8">
            <FormItem :label="L('FatherName')" prop="father">
              <Input v-model="customer.father" />
            </FormItem>
          </i-col>
          <i-col span="16">
            <FormItem :label="L('FatherPhone')" prop="fatherPhone">
              <Input v-model="customer.fatherPhone" />
            </FormItem>
          </i-col>
        </Row>

        <Row :gutter="16">
          <i-col span="8">
            <FormItem :label="L('MotherName')" prop="mother">
              <Input v-model="customer.mother" />
            </FormItem>
          </i-col>
          <i-col span="16">
            <FormItem :label="L('MotherPhone')" prop="motherPhone">
              <Input v-model="customer.motherPhone" />
            </FormItem>
          </i-col>
        </Row>

                <Row :gutter="16">
          <i-col span="8">
            <FormItem :label="L('OtherGuadian')" prop="otherGuadian">
              <Input v-model="customer.otherGuadian" />
            </FormItem>
          </i-col>
          <i-col span="16">
            <FormItem :label="L('OtherGuadianPhone')" prop="otherGuadianPhone">
              <Input v-model="customer.otherGuadianPhone" />
            </FormItem>
          </i-col>
        </Row>
        <FormItem :label="L('Note')" prop="note">
          <Input v-model="customer.note" />
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
import Customer from "@/store/entities/customer";
@Component
export default class CreateCustomer extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  customer: Customer = new Customer();
  save() {
    (this.$refs.customerForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "customer/create",
          data: this.customer
        });
        (this.$refs.customerForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.customerForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  customerRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("StudentName")),
        trigger: "blur"
      }
    ]
  };
}
</script>

