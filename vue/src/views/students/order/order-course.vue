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
          <Row :gutter="16">
            <Col span="3">
              <h3>{{L('OrderName')}}:{{order.name}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('Phone')}}:{{order.phone}}</h3>
            </Col>
            <Col span="3">
              <h3>{{L('Father')}}:{{order.father}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('FatherPhone')}}:{{order.fatherPhone}}</h3>
            </Col>
            <Col span="3">
              <h3>{{L('Mother')}}:{{order.mother}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('MotherPhone')}}:{{order.motherPhone}}</h3>
            </Col>
          </Row>
          <hr
            style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0"
          />
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
        <create-course v-model="createModalShow" @save-success="getpage"></create-course>
        <edit-course v-model="editModalShow" @save-success="getpage"></edit-course>
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
import CreateCourse from "./create-course.vue";
import EditCourse from "./edit-course.vue";
@Component({ components: { CreateCourse, EditCourse } })
export default class OrderCourse extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  order: Order = new Order();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.course.list;
  }
  get loading() {
    return this.$store.state.course.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit() {
    this.editModalShow = true;
  }
  async getpage() {
    await this.$store.dispatch({
      type: "course/getAll",
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
      title: this.L("CourseContent"),
      key: "content"
    },
    {
      title: this.L("Progress"),
      key: "progress"
    },
    {
      title: this.L("CourseDate"),
      key: "date",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.date).toLocaleDateString());
      }
    },
    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
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
                  this.$store.commit("course/edit", params.row);
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

