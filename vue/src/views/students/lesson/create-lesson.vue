<template>
  <div>
    <Modal :title="L('CreateNewLesson')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false">
      <Form ref="lessonForm" label-position="top" :rules="LessonRule" :model="lesson">

        <FormItem :label="L('LessonContent')" prop="content">
          <Input v-model="lesson.content" type="textarea" :rows="3" />
        </FormItem>
        <FormItem :label="L('Progress')" prop="state">
          <Select v-model="lesson.state">
            <Option v-for="n in 3" :key="n" :value="n">{{L(StudentState[n] )}}</Option>
          </Select>
        </FormItem>
        <FormItem :label="L('LessonDate')" prop="date">
          <DatePicker type="date" placeholder="Select date" v-model="lesson.date"></DatePicker>
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
export default class CreateLesson extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  lesson: Lesson = new Lesson();
  student: Student = new Student();

  save() {
    (this.$refs.lessonForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.lesson.studentId = this.student.id;
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
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
    }
  }
  LessonRule = {

    content: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("LessonContent")),
        trigger: "blur"
      }
    ],
    state: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("LessonProgress")),
        trigger: "blur"
      }
    ],
    date: [
      {
        type: "date",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("LessonDate")),
        trigger: "blur"
      }
    ]
  };
}
</script>

