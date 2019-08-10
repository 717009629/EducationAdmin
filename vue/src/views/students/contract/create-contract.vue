<template>
  <div>
    <Modal
      :title="L('CreateNewContract')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      width="1000px"
    >
      <div>
        <Table
          :loading="loading"
          :columns="columns"
          :no-data-text="L('NoDatas')"
          border
          :data="list"
        ></Table>
      </div>
      <hr style="margin:20px 0" />
      <Form ref="contractForm" label-position="top" :rules="ContractRule" :model="contract">
        <Row :gutter="16">
          <Col span="8">
            <FormItem :label="L('StratDate')" prop="startDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.startDate"></DatePicker>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem :label="L('EndDate')" prop="endDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.endDate"></DatePicker>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem :label="L('FullMoney')" prop="fullMoney">
              <InputNumber v-model="contract.fullMoney" style="width:100%"></InputNumber>
            </FormItem>
          </Col>
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
export default class CreateContract extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  contract: Contract = new Contract();
  student: Student = new Student();
  order: Order = new Order();

  get list() {
    return this.$store.state.courseItem.list;
  }
  get loading() {
    return this.$store.state.courseItem.loading;
  }

  async getpage() {
    await this.$store.dispatch({
      type: "courseItem/getAll",
      data: { orderId: this.order.id }
    });
  }

  save() {
    (this.$refs.contractForm as any).validate(async (valid: boolean) => {
      console.log(1);
      if (valid) {
        if (this.list.length === 0) {
          this.$Modal.error({
            title:"Error",
            content: "The Order Must Have At Lease One Course!"
          });
          return;
        }
        this.contract.studentId = this.student.id;
        this.contract.orderId = this.order.id;
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
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      this.order = Util.extend(true, {}, this.$store.state.order.editOrder);
      await this.getpage();

      this.contract.id = this.order.id;
      this.contract.fullMoney = this.list.reduce((number, item) => {
        return number + item.fullMoney;
      }, 0);
    }
  }
  ContractRule = {
    content: [
      {
        required: true,
        message: this.L(
          "FieldIsRequired",
          undefined,
          this.L("ContractContent")
        ),
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
      title: this.L("count"),
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

