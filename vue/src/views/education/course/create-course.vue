<template>
  <div>
    <Modal
      :title="L('CreateNewCourse')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      :transfer="false"
    >
      <Form ref="courseForm" label-position="top" :rules="CourseRule" :model="course">
        <FormItem :label="L('Category')" prop="name">
          <Input v-model="course.category" />
        </FormItem>
        <FormItem :label="L('CourseName')" prop="name">
          <Input v-model="course.name" />
        </FormItem>
        <FormItem :label="L('Price')" prop="name">
          <Input v-model="course.price"  />
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
import Course from "../../../store/entities/course";
@Component
export default class CreateCourse extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  course: Course = new Course();
  student: Student = new Student();
  save() {
    (this.$refs.courseForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "course/create",
          data: this.course
        });
        (this.$refs.courseForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.courseForm as any).resetFields();
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
  CourseRule = {
    content: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("CourseContent")),
        trigger: "blur"
      }
    ]
  };
}
</script>

