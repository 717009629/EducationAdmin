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
        <!-- <FormItem :label="L('LessonNumber')" prop="lessonNumber">
          <Select v-model="lesson.lessonNumber">
            <Option v-for="n in lessonIndexs" :value="n" :key="n" :label="n">
            </Option>
          </Select>
        </FormItem> -->
        <FormItem :label="L('TimePeriod')" prop="timePeriodId">
          <Select v-model="lesson.timePeriodId" @on-change="timeChange">
            <Option v-for="item in timePeriods" :value="item.id" :key="item.id" :label="item.start.slice(0,5)+' - '+item.end.slice(0,5)">
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
        <FormItem :label="L('Subject')" prop="subject">
          <Select v-model="lesson.subject">
            <Option v-for="item in subjects" :value="item.name" :key="item.name" :label="item.name"></Option>
          </Select>
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

  get subjects() {
    return this.$store.state.subject.list;
  }

  get teachers() {
    return this.$store.state.teacher.listAvailable;
  }
  // get periods() {
  //   return this.$store.state.timePeriod.list;
  // }
  get timePeriods() {
    return this.$store.state.timePeriod.listAvailable;
  }
  async timeChange() {
    await this.$store.dispatch({
      type: "teacher/getAllAvailable",
      data: { date: this.date, timePeriodId: this.lesson.timePeriodId }
    });
  }

  @Watch("teachers")
  watchTeacher(n, o) {
    if (n.filter(m => m.id === this.lesson.teacherId).length === 0) {
      this.lesson.teacherId = undefined;
    }
  }
  //   get timePeriods() {
  //   let list = [];
  //   let lessons = this.$store.state.lesson.list;
  //   let array = lessons
  //     .filter(
  //       m =>
  //         this.date &&
  //         new Date(m.lessonDate).toDateString() === this.date.toDateString()
  //     )
  //     .map(m => m.timePeriodId);

  //   for (var n = 0; n < this.periods.length; n++) {
  //     if (array.indexOf(this.periods[n].id) < 0) {
  //       list.push(this.periods[n]);
  //     }
  //   }
  //   return list;
  // }

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
      this.lesson.teacherId = undefined;
    } else {
      this.clas = Util.extend(true, {}, this.$store.state.class.editClass);
      this.lesson.classId = this.clas.id;
      this.lesson.lessonDate = this.date;
      await this.$store.dispatch({
        type: "teacher/getAllAvailable",
        data: { date: this.date, timePeriodId: this.lesson.timePeriodId }
      });
      await this.$store.dispatch({
        type: "timePeriod/getAllAvailable",
        data: { date: this.date, classId: this.clas.id }
      });
    }
  }
  created() {
    this.$store.dispatch({
      type: "subject/getAll",
      data: { maxResultCount: 10000, isActive: true }
    });
  }
  LessonRule = {
    subject: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Subject")),
        trigger: "blur"
      }
    ],
    timePeriodId: [
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

