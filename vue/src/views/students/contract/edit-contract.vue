<template>
  <div>
    <Modal :title="L('EditContract')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" >
      <Form ref="contractForm" label-position="top" :rules="ContractRule" :model="contract">
        <Row :gutter="16">
          <Col span="12">
            <FormItem :label="L('StratDate')" prop="startDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.StratDate"></DatePicker>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem :label="L('EndDate')" prop="endDate">
              <DatePicker type="date" placeholder="Select date" v-model="contract.endDate"></DatePicker>
            </FormItem>
          </Col>
        </Row>

        <FormItem :label="L('FullMoney')" prop="fullMony">
          <InputNumber v-model="contract.fullMony"   style="width:100%"/>
        </FormItem>

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
@Component
export default class EditContracte extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  contract: Contract = new Contract();
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
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.contract = Util.extend(true, {}, this.$store.state.contract.editContract);
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

