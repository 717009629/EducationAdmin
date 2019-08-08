<template>
  <div>
    <Modal
      :title="L('EditCourse')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
     class="modal-thrid"
    >
      <Form ref="courseForm" label-position="top" :rules="CourseRule" :model="course">
        <FormItem :label="L('LargeCategory')" prop="name">
          <Input v-model="course.largeCategory" />
        </FormItem>
        <FormItem :label="L('SubCategory')" prop="name">
          <Input v-model="course.subCategory" />
        </FormItem>
        <FormItem :label="L('CourseName')" prop="name">
          <Input v-model="course.name" />
        </FormItem>
        <FormItem :label="L('Price')" prop="name">
          <Input v-model="course.price" />
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
import Course from "../../../store/entities/course";
@Component
export default class EditCoursee extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  course: Course = new Course();
  save() {
    (this.$refs.courseForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "course/update",
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
      this.course = Util.extend(true, {}, this.$store.state.course.editCourse);
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

