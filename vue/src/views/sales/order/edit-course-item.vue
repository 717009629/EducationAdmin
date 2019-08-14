<template>
  <div>
    <Modal
      :title="L('EditCourseItem')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
      :mask-closable="false"
    >
      <Form ref="courseItemForm" label-position="top" :rules="CourseItemRule" :model="courseItem">
        <FormItem :label="L('Course')" prop="courseId">
          <Select v-model="courseItem.courseId" filterable>
            <Option
              v-for="item in courseList"
              :value="item.id"
              :key="item.id"
              :label="item.category+'：'+item.name+'：'+item.price+'元'"
            >
              <span>{{item.category}}</span>
              <span>：</span>
              <span>{{item.name}}</span>
              <span style="float:right;">{{item.price}}元</span>
            </Option>
          </Select>
        </FormItem>
        <FormItem :label="L('Count')" prop="count">
          <InputNumber v-model="courseItem.count" style="width:100%" :min="1" />
        </FormItem>
        <FormItem :label="L('FullMoney')" prop="fullMoney">
          <Input :value="fullMoney" disabled />
        </FormItem>

        <FormItem :label="L('Note')" prop="note">
          <Input v-model="courseItem.note" />
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
import CourseItem from "../../../store/entities/courseItem";
@Component
export default class EditCourseIteme extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  courseItem: CourseItem = new CourseItem();

  get fullMoney() {
    if (this.courseItem.courseId) {
      var course = this.courseList.filter(
        m => m.id === this.courseItem.courseId
      )[0];
      if (course) {
        if (this.courseItem.count) {
          return course.price * this.courseItem.count;
        }
      }
    }
    return 0;
    // return this.courseItem
  }
  get courseList() {
    return this.$store.state.course.list;
  }

  save() {
    (this.$refs.courseItemForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "courseItem/update",
          data: this.courseItem
        });
        (this.$refs.courseItemForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.courseItemForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.courseItem = Util.extend(
        true,
        {},
        this.$store.state.courseItem.editCourseItem
      );
      this.getAllCourseItems();
    }
  }

  async getAllCourseItems() {
    await this.$store.dispatch({
      type: "course/getAll"
    });
  }

  CourseItemRule = {
    courseId: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Course")),
        trigger: "change"
      }
    ],
    count: [
      {
        type: "number",
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Count")),
        trigger: "blur"
      }
    ]
  };
}
</script>

