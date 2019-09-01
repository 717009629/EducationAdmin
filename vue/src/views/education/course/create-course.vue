<template>
  <div>
    <Modal :title="L('CreateNewCourse')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :mask-closable="false" :transfer="false">
      <Form ref="courseForm" label-position="top" :rules="CourseRule" :model="course">
        <FormItem :label="L('ClassType')" prop="classType">
          <Select v-model="course.classType" :transfor='false'>
            <Option v-for="item in classType" :key="item.key" :value="item.key" :label=" L(item.value)" ></Option>
          </Select>
        </FormItem>

        <FormItem :label="L('Category')" prop="category">
          <Input v-model="course.category" />
        </FormItem>
        <FormItem :label="L('CourseName')" prop="name">
          <Input v-model="course.name" />
        </FormItem>
        <FormItem :label="L('Price')" prop="price">
          <InputNumber v-model="course.price" style="width:100%" />
        </FormItem>
        <FormItem :label="L('Note')" prop="note">
          <Input v-model="course.note" type="textarea" :rows="3" />
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
  classType=window.abp.custom.classType;
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
    }
  }
  CourseRule = {
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("CourseName")),
        trigger: "blur"
      }
    ],
    category: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("CourseCategory")),
        trigger: "blur"
      }
    ],
    classType: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("ClassType")),
        trigger: "blur"
      }
    ],
    price: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Price")),
        trigger: "blur"
      }
    ]
  };
}
</script>

