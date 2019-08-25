<template>
  <div>
    <Modal :title="L('EditTimePeriod')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="timePeriodForm" label-position="top" :rules="TimePeriodRule" :model="timePeriod">
        <Row :gutter="16">
          <i-col span=12>
            <FormItem :label="L('Start')" prop="start">
              <TimePicker type="time" placeholder="Select time" v-model="timePeriod.start" style="100%" format="HH:mm" :steps="[1, 5]" disabled></TimePicker>
            </FormItem>
          </i-col>

          <i-col span=12>
            <FormItem :label="L('End')" prop="end">
              <TimePicker type="time" placeholder="Select time" v-model="timePeriod.end" style="100%" format="HH:mm" :steps="[1, 5]" disabled></TimePicker>
            </FormItem>
          </i-col>
        </Row>
        <FormItem>
          <Checkbox v-model="timePeriod.isActive">{{L('IsActive')}}</Checkbox>
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
import TimePeriod from "../../../store/entities/timePeriod";
@Component
export default class EditTimePeriode extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  timePeriod: TimePeriod = new TimePeriod();
  save() {
    (this.$refs.timePeriodForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "timePeriod/update",
          data: this.timePeriod
        });
        (this.$refs.timePeriodForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.timePeriodForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.timePeriod = Util.extend(
        true,
        {},
        this.$store.state.timePeriod.editTimePeriod
      );
    }
  }
  TimePeriodRule = {
    start: [
      {
        type: "string",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Start")),
        trigger: "blur"
      }
    ],
    end: [
      {
        type: "string",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("End")),
        trigger: "blur"
      }
    ]
  };
}
</script>

