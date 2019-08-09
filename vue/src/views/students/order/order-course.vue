<template>
  <div>
    <Modal
      :title="L('CourseList')"
      :value="value"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      width="1100px"
      class="modal-second"
    >
      <div>
        <!-- <Card dis-hover> -->

        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table
            :loading="loading"
            :columns="columns"
            :no-data-text="L('NoDatas')"
            border
            :data="list"
          ></Table>
        </div>
        <!-- </Card> -->
        <create-course-item v-model="createModalShow" @save-success="getpage"></create-course-item>
        <edit-course-item v-model="editModalShow" @save-success="getpage"></edit-course-item>
      </div>
      <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Order from "../../../store/entities/order";
import Course from "../../../store/entities/course";
import CreateCourseItem from "./create-course-item.vue";
import EditCourseItem from "./edit-course-item.vue";
@Component({ components: { CreateCourseItem, EditCourseItem } })
export default class OrderCourse extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  order: Order = new Order();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.courseItem.list;
  }
  get loading() {
    return this.$store.state.courseItem.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit() {
    this.editModalShow = true;
  }
  async getpage() {
    await this.$store.dispatch({
      type: "courseItem/getAll",
      data: { orderId: this.order.id }
    });
  }

  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.order = Util.extend(true, {}, this.$store.state.order.editOrder);
      this.getpage();
    }
  }

  columns = [
    // {
    //   title: this.L("OrderName"),
    //   key: "name"
    // },
    {
      title: this.L("CourseCategory"),
      key: "courseCategory"
    },
    {
      title: this.L("CourseName"),
      key: "courseName"
    },
    {
      title: this.L("count"),
      key: "count"
    },
    {
      title: this.L("FullMoney"),
      key: "fullMoney"
    },
    {
      title: this.L("Note"),
      key: "note"
    },

    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      render: (h: any, params: any) => {
        return h("div", [
          h(
            "Button",
            {
              props: {
                type: "primary",
                size: "small"
              },
              style: {
                marginRight: "5px"
              },
              on: {
                click: () => {
                  this.$store.commit("courseItem/edit", params.row);
                  this.edit();
                }
              }
            },
            this.L("Edit")
          )
        ]);
      }
    }
  ];
}
</script>

