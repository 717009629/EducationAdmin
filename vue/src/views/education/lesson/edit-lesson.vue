<template>
  <div>
    <Modal :title="L('EditLesson')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="lessonForm" label-position="top" :rules="LessonRule" :model="lesson">
        <FormItem :label="L('LessonDate')">
          <DatePicker type="date" placeholder="Select date" readonly :value="lesson.lessonDate"></DatePicker>
        </FormItem>
        <FormItem :label="L('TimePeriod')" prop="timePeriodId">
          <Select v-model="lesson.timePeriodId" @on-change="timeChange">
            <Option v-for="item in timePeriods" :value="item.id" :key="item.id" :label="item.start.slice(0,5)+' - '+item.end.slice(0,5)">
            </Option>
          </Select>
        </FormItem>

        <FormItem :label="L('Teacher')" prop="teacherId">
          <Select v-model="lesson.teacherId" filterable>
            <Option v-for="item in teachers" :value="item.id" :key="item.id" :label="item.id+'  ' +item.name">
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
@Component
export default class EditLessone extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  lesson: Lesson = new Lesson();
  //lessonIndexs=[];

  get orders() {
    return this.$store.state.order.list;
  }

  get teachers() {
    let teachers = this.$store.state.teacher.listAvailable;
    //console.log(this.lesson.teacherId);
    // console.log(teachers);
    return teachers;
  }
  @Watch("teachers")
  watchTeacher(n,o) {
    if (n.filter(m => m.id === this.lesson.teacherId).length === 0) {
      this.lesson.teacherId = undefined;
    }
  }

  get timePeriods() {
    return this.$store.state.timePeriod.listAvailable;
  }
  async timeChange() {
    await this.$store.dispatch({
      type: "teacher/getAllAvailable",
      data: {
        date: this.lesson.lessonDate,
        timePeriodId: this.lesson.timePeriodId,
        lessonId: this.lesson.id
      }
    });
  }
  // clearTeacher() {
  //   let teachers = this.$store.state.teacher.listAvailable;
  //   if (teachers.filter(m => m.id === this.lesson.teacherId).length === 0) {
  //     this.lesson.teacherId = undefined;
  //   }
  // }
  save() {
    (this.$refs.lessonForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "lesson/update",
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
      this.lesson = new Lesson();
      this.$emit("input", value);
    } else {
      this.lesson = Util.extend(true, {}, this.$store.state.lesson.editLesson);
      //this.setLessonIndexs();
      await this.$store.dispatch({
        type: "teacher/getAllAvailable",
        data: {
          date: this.lesson.lessonDate,
          timePeriodId: this.lesson.timePeriodId,
          lessonId: this.lesson.id
        }
      });
      await this.$store.dispatch({
        type: "timePeriod/getAllAvailable",
        data: {
          date: this.lesson.lessonDate,
          classId: this.lesson.classId,
          lessonId: this.lesson.id
        }
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
    timePeriod: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("TimePeriod")),
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

