<template>
  <div>
    <Form ref="queryForm" :label-width="100" label-position="left" inline>
      <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
      <Row>
        <Button @click="create" icon="android-add" type="primary" >{{L('Add')}}</Button>
      </Row>
    </Form>
    <!-- <Card dis-hover> -->
    <div class="margin-top-10">
      <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list"></Table>
    </div>
    <!-- </Card> -->
    <create-order v-model="createModalShow" @save-success="getpage"></create-order>
    <edit-order v-model="editModalShow" @save-success="getpage"></edit-order>
    <order-course v-model="courseModalShow"></order-course>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Order from "../../../store/entities/order";
import CreateOrder from "../order/create-order.vue";
import EditOrder from "../order/edit-order.vue";
import OrderCourse from "../order/order-course.vue";
@Component({ components: { CreateOrder, EditOrder, OrderCourse } })
export default class StudentOrder extends AbpBase {
  @Prop({ type: Number, default: null }) studentId: null;
  student: Student = new Student();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  courseModalShow: boolean = false;
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit() {
    this.editModalShow = true;
  }
  showCourse() {
    this.courseModalShow = true;
  }
  async getpage() {
    await this.$store.dispatch({
      type: "order/getAll",
      data: { studentId: this.student.id }
    });
  }
  @Watch("studentId")
  async watchId(nv, ov) {
    if (nv) {
      await this.showPage();
    }
  }

  async showPage() {
    this.student = Util.extend(true, {}, this.$store.state.student.editStudent);
    await this.getpage();
  }

  columns = [
    {
      title: this.L("Index"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
    },

    {
      title: this.L("OrderDate"),
      key: "orderDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.date).toLocaleDateString());
      }
    },
    {
      title: this.L("SchoolBegin"),
      key: "schoolBegin",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.date).toLocaleDateString());
      }
    },
    {
      title: this.L("FullMomeny"),
      key: "fullMomeny"
    },
    {
      title: this.L("ClassName"),
      key: "className"
    },
    {
      title: this.L("State"),
      key: "state"
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
                  this.$store.commit("order/edit", params.row);
                  this.edit();
                }
              }
            },
            this.L("Edit")
          ),
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
                  this.$store.commit("order/edit", params.row);
                  this.showCourse();
                }
              }
            },
            this.L("Course")
          )
        ]);
      }
    }
  ];
}
</script>

