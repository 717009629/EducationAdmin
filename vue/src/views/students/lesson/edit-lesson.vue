<template>
  <div>
    <Modal :title="L('EditLesson')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="lessonForm" label-position="top" :rules="LessonRule" :model="lesson">
        <FormItem :label="L('LessonDate')">
          <DatePicker type="date" placeholder="Select date" readonly :value="lesson.lessonDate"></DatePicker>
        </FormItem>
        <FormItem :label="L('LessonIndex')" prop="lessonIndex">
          <Select v-model="lesson.lessonIndex" filterable>
            <Option v-for="n in 8" :value="n" :key="n" :label="n">
            </Option>
          </Select>
        </FormItem>
        <FormItem :label="L('Order')" prop="orderId">
          <Select v-model="lesson.orderId" filterable>
            <Option v-for="item in orders" :value="item.id" :key="item.id" :label="item.id+'  ' +item.course.name">
              <span>{{item.id}}</span>
              <span style="margin-left:10px">{{item.course.name}}</span>
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
        <FormItem :label="L('Course')" prop="course">
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
import Student from "../../../store/entities/student";
import Lesson from "../../../store/entities/lesson";
@Component
export default class EditLessone extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  lesson: Lesson = new Lesson();

  get orders() {
    return this.$store.state.order.list;
  }

  get teachers() {
    return this.$store.state.teacher.list;
  }

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
      this.$emit("input", value);
    } else {
      this.lesson = Util.extend(true, {}, this.$store.state.lesson.editLesson);
      await this.$store.dispatch({
        type: "teacher/getAll"
      });
      await this.$store.dispatch({
        type: "order/getAll",
        data: { studentId: this.lesson.studentId }
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

