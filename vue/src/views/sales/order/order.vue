<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="8">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.studentName" :placeholder="L('StudentName')" />
              </FormItem>
            </i-col>
            <i-col span="8">
              <Button icon="ios-search" type="primary" size="large" @click="pageChange(1)" class="toolbar-btn" v-if="hasPermission('Pages.Orders.Create')">{{L('Find')}}</Button>
            </i-col>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" :row-class-name="rowClassName">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Orders.Edit')">
              <Button v-if="hasPermission('Pages.Orders.Edit')&&row.state===0" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button v-if="hasPermission('Pages.Orders.Audite')&&row.state===0" type="primary" size="small" @click="audite(row)" style="margin-right:5px">{{L('Audite')}}</Button>
              <Button v-if="row.state!==0" type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('LessonAttendance')}}</Button>
              <Button v-if="hasPermission('Pages.Orders.Finish')&&row.state===1" type="primary" size="small" @click="finish(row)" style="margin-right:5px">{{L('FinishLesson')}}</Button>
              <!-- <Button v-if="hasPermission('Pages.Orders.Edit')&&!row.contract" type="primary" size="small" @click="showContract(row)" style="margin-right:5px">{{L('ConvertContract')}}</Button> -->
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <edit-order v-model="editModalShow" @save-success="getpage"></edit-order>
    <lesson-attendance v-model="lessonAttendanceModalShow" @save-success="getpage"></lesson-attendance>
    <!-- <create-contract v-model="contractModalShow"></create-contract> -->
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditOrder from "./edit-order.vue";
import LessonAttendance from "./lesson-attendance.vue";
// import CreateContract from "../contract/create-contract.vue";

class PageOrderRequest extends PageRequest {
  studentName: string = "";
}

@Component({
  components: { EditOrder, LessonAttendance }
})
export default class Orders extends AbpBase {
  pagerequest: PageOrderRequest = new PageOrderRequest();

  editModalShow: boolean = false;
  lessonAttendanceModalShow: boolean = false;
  // contractModalShow: boolean = false;
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  edit(row) {
    this.$store.commit("order/edit", row);
    this.editModalShow = true;
  }
  lesson(row) {
    this.$store.commit("order/edit", row);
    this.lessonAttendanceModalShow = true;
  }
  // showContract(row) {
  //   this.$store.commit("order/edit", row);
  //   this.contractModalShow = true;
  // }

  pageChange(page: number) {
    this.$store.commit("order/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("order/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "order/getAll",
      data: this.pagerequest
    });
  }
  audite(row) {
    this.$Modal.confirm({
      title: this.L("Confirm"),
      content: this.L(
        "After audited the order can not be edited, determine the audit?"
      ),
      onOk: () => {
        setTimeout(async () => {
          await this.$store.dispatch({
            type: "order/audite",
            data: { orderId: row.id }
          });
          await this.getpage();
        }, 100);
      }
    });
  }
  finish(row) {
    let msg = "";
    var finished = row.lessonAttendances.filter(m => m.attended).length;
    this.$Modal.confirm({
      title: this.L("Confirm"),
      content: this.L(
        "OrderFinisLessonConfirm",
        undefined,
        row.count,
        finished
      ),
      onOk: () => {
        setTimeout(async () => {
          await this.$store.dispatch({
            type: "order/finish",
            data: { orderId: row.id }
          });
          await this.getpage();
        }, 200);
      }
    });
  }

  get pageSize() {
    return this.$store.state.order.pageSize;
  }
  get totalCount() {
    return this.$store.state.order.totalCount;
  }
  get currentPage() {
    return this.$store.state.order.currentPage;
  }
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
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      },
      width: 80
    },

    {
      title: this.L("StudentIndex"),
      key: "sutdentId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.student.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h, params) => h("span", params.row.student.name)
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
      title: this.L("CourseName"),
      key: "courseName",
      render: (h, params) => {
        return h("span", params.row.course.name);
      }
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
    {
      title: this.L("FullMoney"),
      key: "fullMoney"
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
      }
    },
    {
      title: this.L("LessonCount"),
      key: "count"
    },
    {
      title: this.L("AttendedLesson"),
      key: "attendedLesson",
      render: (h, params) => {
        return h(
          "span",
          params.row.lessonAttendances.filter(m => m.attended).length
        );
      }
    },
    {
      title: this.L("AbsentLesson"),
      key: "absentLesson",
      render: (h, params) => {
        return h(
          "span",
          params.row.lessonAttendances.filter(m => !m.attended).length
        );
      }
    },
    {
      title: this.L("Note"),
      key: "note",
      tooltip: true
    },
    {
      title: this.L("AuditeDate"),
      key: "auditeTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.auditeTime
            ? new Date(params.row.auditeTime).toLocaleDateString()
            : ""
        );
      }
    },
    {
      title: this.L("FinishLessonDate"),
      key: "finishTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.finishTime
            ? new Date(params.row.finishTime).toLocaleDateString()
            : ""
        );
      }
    },

    // {
    //   title: this.L("SalesmanName"),
    //   key: "salesmanName"
    // },

    {
      title: this.L("Actions"),
      key: "Actions",
      width: 190,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
  }
}
</script>