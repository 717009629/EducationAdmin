<template>
  <Modal :title="L('Order')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1300px">
    <Form ref="queryForm" :label-width="100" label-position="left" inline>
      <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
      <Row>
        <Button @click="create" icon="android-add" type="primary" v-if="hasPermission('Pages.Orders.Create')">{{L('Add')}}</Button>
      </Row>
    </Form>
    <!-- <Card dis-hover> -->
    <div class="margin-top-10">
      <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" :row-class-name="rowClassName">
        <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Orders.Edit')">
          <Button v-if="hasPermission('Pages.Orders.Edit')&&row.state===0" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
          <Button v-if="hasPermission('Pages.Orders.Audite')&&row.state===0" type="primary" size="small" @click="audite(row)" style="margin-right:5px">{{L('Audite')}}</Button>
          <Button v-if="row.state!==0" type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('LessonAttendance')}}</Button>
          <!-- <Button v-if="hasPermission('Pages.Orders.Edit')" type="primary" size="small" @click="showContract(row)" style="margin-right:5px">{{L('ConvertContract')}}</Button> -->
        </template>
      </Table>
    </div>
    <!-- </Card> -->
    <create-order v-model="createModalShow" @save-success="getpage"></create-order>
    <edit-order v-model="editModalShow" @save-success="getpage"></edit-order>
    <lesson-attendance v-model="lessonAttendanceModalShow" @save-success="getpage"></lesson-attendance>

    <!-- <create-contract v-model="contractModalShow" @save-success="refreshOrderAndContract"></create-contract> -->
  </Modal>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Order from "../../../store/entities/order";
import CreateOrder from "../../sales/order/create-order.vue";
import EditOrder from "../../sales/order/edit-order.vue";
import LessonAttendance from "../../sales/order/lesson-attendance.vue";

//import CreateContract from "../../sales/contract/create-contract.vue";
@Component({
  components: { CreateOrder, EditOrder, LessonAttendance }
})
export default class StudentOrder extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  // @Prop({ type: Number, default: null }) studentId: null;
  student: Student = new Student();
  createModalShow: boolean = false;
  editModalShow: boolean = false;
  lessonAttendanceModalShow: boolean = false;
  //contractModalShow: boolean = false;
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("order/edit", row);
    this.editModalShow = true;
  }
  lesson(row) {
    this.$store.commit("order/edit", row);
    this.lessonAttendanceModalShow = true;
  }
  audite(row) {
    this.$Modal.confirm({
      title: this.L("Confirm"),
      content: this.L(
        "After the audit can not be edited, determine the audit?"
      ),
      onOk: async () => {
        await this.$store.dispatch({
          type: "order/audite",
          data: { orderId: row.id }
        });
        await this.getpage();
      }
    });
  }
  // showContract(row) {
  //   this.$store.commit("order/edit", row);
  //   this.contractModalShow = true;
  // }
  // async refreshOrderAndContract() {
  //   this.$emit("goContract");
  //   await this.$store.dispatch({
  //     type: "contract/getAll",
  //     data: { studentId: this.student.id }
  //   });
  //   await this.getpage();
  // }
  async getpage() {
    await this.$store.dispatch({
      type: "order/getAll",
      data: { studentId: this.student.id }
    });
  }
  // @Watch("studentId")
  // async watchId(nv, ov) {
  //   if (nv) {
  //     await this.showPage();
  //   }
  // }
  async visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      await this.getpage();
    }
  }

  // async showPage() {
  //   this.student = Util.extend(true, {}, this.$store.state.student.editStudent);
  //   await this.getpage();
  // }
  rowClassName(row, index) {
    if (row.state === 2) {
      return "success";
    }
    if (row.lessonAttendances.filter(m => m.attended).length > row.count) {
      return "error";
    }
    if (row.lessonAttendances.filter(m => m.attended).length === row.count) {
      return "warning";
    }
    return "";
  }
  columns = [
    {
      title: this.L("Index"),
      key: "name",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      },
      width: 80
    },

    {
      title: this.L("OrderDate"),
      key: "orderDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.orderDate).toLocaleDateString());
      }
    },
    // {
    //   title: this.L("SchoolBegin"),
    //   key: "schoolBegin",
    //   render: (h: any, params: any) => {
    //     return h("span", new Date(params.row.schoolBegin).toLocaleDateString());
    //   }
    // },

    {
      title: this.L("Course"),
      key: "course",
      render: (h, params) => {
        return h("span", params.row.course.name);
      },
      width: 120
    },
    {
      title: this.L("ClassType"),
      key: "classType",
      render: (h, params) => {
        return h(
          "span",
          this.L(window.abp.custom.ClassType[params.row.course.classType])
        );
      }
    },
    {
      title: this.L("CoursePrice"),
      key: "coursePrice",
      render: (h, params) => {
        return h("span", params.row.course.price);
      }
    },

    // {
    //   title: this.L("State"),
    //   key: "state"
    // },

    {
      title: this.L("FullMoney"),
      key: "fullMoney"
    },
    {
      title: this.L("Note"),
      key: "note",
      tooltip: true
    },
    {
      title: this.L("State"),
      key: "state",
      render: (h, params) => {
        return h(
          "span",
          this.L(window.abp.custom.OrderState[params.row.state])
        );
      }
    },
    {
      title: this.L("Class"),
      key: "class",
      render: (h, params) => {
        return h("span", params.row.class ? params.row.class.name : "");
      },
      width: 150
    },
    {
      title: this.L("LessonCount"),
      key: "count",
      width: 65
    },
    {
      title: this.L("AttendedLesson"),
      key: "attendedLesson",
      render: (h, params) => {
        return h(
          "span",
          params.row.lessonAttendances.filter(m => m.attended).length
        );
      },
      width: 65
    },
    {
      title: this.L("AbsentLesson"),
      key: "absentLesson",
      render: (h, params) => {
        return h(
          "span",
          params.row.lessonAttendances.filter(m => !m.attended).length
        );
      },
      width: 65
    },
    // {
    //   title: this.L("SalesmanName"),
    //   key: "salesmanName",
    //   width: 80
    // },

    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      slot: "action"
    }
  ];
}
</script>

