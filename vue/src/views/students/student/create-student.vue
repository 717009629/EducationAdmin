<template>
  <div>
    <Modal :title="L('CreateNewStudent')" :value="value" @on-ok="save" @on-visible-change="visibleChange">
      <Form ref="studentForm" label-position="top" :rules="studentRule" :model="student">
        <Tabs value="detail">
          <TabPane :label="L('BasicInfo')" name="detail">
            <FormItem :label="L('StudentName')" prop="name">
              <Input v-model="student.name" :maxlength="32" :minlength="2" />
            </FormItem>
            <FormItem :label="L('Sex')" prop="sex">
              <RadioGroup v-model="sex" type="button">
                <Radio label="male">{{L('Male')}}</Radio>
                <Radio label="female">{{L('Female')}}</Radio>
              </RadioGroup>
            </FormItem>
            <FormItem :label="L('Birthday')" prop="birthday">
              <DatePicker type="date" placeholder="Select date" v-model="student.birthday"></DatePicker>
            </FormItem>
            <FormItem :label="L('School')" prop="school">
              <Input v-model="student.school" />
            </FormItem>
            <FormItem :label="L('Grade')" prop="grade">
              <Input v-model="student.grade" />
            </FormItem>
          </TabPane>

          <TabPane :label="L('Location')" name="address">
            <FormItem :label="L('Province')" prop="province">
              <Input v-model="student.province" />
            </FormItem>
            <FormItem :label="L('City')" prop="city">
              <Input v-model="student.city" />
            </FormItem>
            <FormItem :label="L('Address')" prop="address">
              <Input v-model="student.address" />
            </FormItem>
          </TabPane>
          <TabPane :label="L('Contact')" name="contact">
            <FormItem :label="L('Parent')" prop="parent">
              <Input v-model="student.parent" />
            </FormItem>
            <FormItem :label="L('Relation')" prop="relation">
              <Input v-model="student.relation" />
            </FormItem>
            <FormItem :label="L('Phone')" prop="phone">
              <Input v-model="student.phone" />
            </FormItem>
            <Row :gutter="16">
              <i-col span="8">
                <FormItem :label="L('FatherName')" prop="father">
                  <Input v-model="student.father" />
                </FormItem>
              </i-col>
              <i-col span="16">
                <FormItem :label="L('FatherPhone')" prop="fatherPhone">
                  <Input v-model="student.fatherPhone" />
                </FormItem>
              </i-col>
            </Row>

            <Row :gutter="16">
              <i-col span="8">
                <FormItem :label="L('MotherName')" prop="mother">
                  <Input v-model="student.mother" />
                </FormItem>
              </i-col>
              <i-col span="16">
                <FormItem :label="L('MotherPhone')" prop="motherPhone">
                  <Input v-model="student.motherPhone" />
                </FormItem>
              </i-col>
            </Row>
          </TabPane>

          <TabPane :label="L('ElseInfo')" name="else">
            <FormItem :label="L('CourseType')" prop="courseType">
              <Input v-model="student.courseType" />
            </FormItem>
            <FormItem :label="L('StudentType')" prop="studentType">
              <Input v-model="student.studentType" />
            </FormItem>
            <FormItem :label="L('TeachMethod')" prop="teachMethod">
              <Input v-model="student.teachMethod" />
            </FormItem>
            <FormItem :label="L('Origin')" prop="origin">
              <Input v-model="student.origin" />
            </FormItem>
            <FormItem :label="L('Note')" prop="note">
              <Input v-model="student.note" />
            </FormItem>
          </TabPane>
        </Tabs>
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
import Student from "@/store/entities/student";
@Component
export default class CreateStudent extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
  get sex() {
    return this.student.sex
      ? "male"
      : this.student.sex === false
      ? "female"
      : "";
  }
  set sex(val) {
    this.student.sex = val === "male" ? true : val === "female" ? false : null;
  }
  save() {
    (this.$refs.studentForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "student/create",
          data: this.student
        });
        (this.$refs.studentForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.studentForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  studentRule = {
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

