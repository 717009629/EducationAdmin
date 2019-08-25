<template>
  <div>
    <Modal :title="L('CreateNewClass')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="classForm" label-position="top" :rules="ClassRule" :model="clas">

        <FormItem :label="L('ClassName')" prop="name">
          <Input v-model="clas.name" />
        </FormItem>

        <FormItem :label="L('Course')" prop="courseId">
          <Select v-model="clas.courseId">
            <Option v-for="c in courses" :value="c.id" :key="c.id" :label="''+ c.id+'--' +c.name">
              <!-- <span>{{c.id}}</span>
              <span>--</span>
              <span>{{c.name}}</span>
              <span>--</span>
              <span>{{c.classType===0?L('OneToMany'):c.classType===1?L('OneToOne'):""}}</span> -->
            </Option>
          </Select>
        </FormItem>

        <FormItem :label="L('Teacher')" prop="teacherId">
          <Select v-model="clas.teacherId">
            <Option v-for="item in teachers" :value="item.id" :key="item.id" :label="''+ item.id+'  ' +item.name">
              <span>{{item.id}}</span>
              <span style="margin-left:10px">{{item.name}}</span>
            </Option>
          </Select>
        </FormItem>
        <FormItem :label="L('Note')" prop="note">
          <Input v-model="clas.note" type="textarea" :rows="3" />
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
import Class from "../../../store/entities/class";
import TimePeriod from "../../../store/entities/timePeriod";
@Component
export default class CreateClass extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  clas: Class = new Class();

  get teachers() {
    return this.$store.state.teacher.list;
  }
  get courses() {
    return this.$store.state.course.list;
  }
  save() {
    (this.$refs.classForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "class/create",
          data: this.clas
        });
        (this.$refs.classForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.classForm as any).resetFields();
    this.$emit("input", false);
  }
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      await this.$store.dispatch({
        type: "teacher/getAll"
      });
      await this.$store.dispatch({
        type: "course/getAll",
        data: { classType: 0 }
      });
    }
  }
  ClassRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("ClassName")),
        trigger: "blur"
      }
    ],
    courseId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Course")),
        trigger: "blur"
      }
    ],
    teacherId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("HeadTeacher")),
        trigger: "blur"
      }
    ]
  };
}
</script>

