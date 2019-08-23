<template>
  <div>
    <Modal :title="L('CreateNewLesson')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="lessonForm" label-position="top" :rules="LessonRule" :model="lesson">
        <FormItem :label="L('LessonDate')">
          <DatePicker type="date" placeholder="Select date" readonly :value="lesson.lessonDate"></DatePicker>
        </FormItem>

        <!-- <FormItem :label="L('LessonTime')">
          <TimePicker  format="HH:mm" placeholder="Select time" :steps="[1, 5]"  :value="lesson.lessonDate"></TimePicker>
        </FormItem> -->
        <FormItem :label="L('LessonNumber')" prop="lessonNumber">
          <Select v-model="lesson.lessonNumber" >
            <Option v-for="n in lessonIndexs" :value="n" :key="n" :label="n">
            </Option>
          </Select>
        </FormItem>


        <FormItem :label="L('Teacher')" prop="teacherId">
          <Select v-model="lesson.teacherId" filterable>
            <Option v-for="item in teachers" :value="item.id" :key="item.id" :label="''+ item.id+'  ' +item.name">
              <span>{{item.id}}</span>
              <span style="margin-left:10px">{{item.name}}</span>
            </Option>
          </Select>
        </FormItem>
        <FormItem :label="L('CourseItem')" prop="course">
          <Input v-model="lesson.course" />
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
import Class from "../../../store/entities/class";
import Lesson from "../../../store/entities/lesson";
import Order from "../../../store/entities/order";
import User from "../../../store/entities/user";
@Component
export default class CreateLesson extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  @Prop({ type: Date }) date: Date;
  lesson: Lesson = new Lesson();
  clas: Class = new Class();

  get orders() {
    return this.$store.state.order.list;
  }

  get teachers() {
    return this.$store.state.teacher.list;
  }
  get lessonIndexs() {
    let list = [];
    let lessons = this.$store.state.lesson.list;
    for (var n = 1; n <= 8; n++) {
      let count = lessons.filter(
        m =>
          this.date &&
          new Date(m.lessonDate).toDateString() === this.date.toDateString() &&
          m.lessonNumber === n
      ).length;
      if (count < 1) {
        list.push(n);
      }
    }
    return list;
  }

  save() {
    (this.$refs.lessonForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.lesson.classId = this.clas.id;
        await this.$store.dispatch({
          type: "lesson/create",
          data: this.lesson
        });
        (this.$refs.lessonForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.lessonForm as any).resetFields();
    this.$emit("input", false);
  }
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.clas = Util.extend(
        true,
        {},
        this.$store.state.class.editClass
      );
      this.lesson.classId = this.clas.id;
      this.lesson.lessonDate = this.date;
      await this.$store.dispatch({
        type: "teacher/getAll"
      });
      await this.$store.dispatch({
        type: "order/getAll",
        data: { classId: this.clas.id }
      });
    }
  }
  LessonRule = {
    course: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Course")),
        trigger: "blur"
      }
    ],
    lessonNumber: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("LessonNumber")),
        trigger: "blur"
      }
    ],
    orderId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Order")),
        trigger: "blur"
      }
    ],
    teacherId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Teacher")),
        trigger: "blur"
      }
    ]
  };
}
</script>

