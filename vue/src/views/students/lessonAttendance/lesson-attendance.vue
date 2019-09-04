<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <!-- <Button v-if="hasPermission('Pages.LessonAttendances.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button> -->
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <!-- <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.LessonAttendances.Edit')">
              <Button v-if="hasPermission('Pages.LessonAttendances.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template> -->
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <!-- <create-lessonAttendance v-model="createModalShow" @save-success="getpage"></create-lessonAttendance>
    <edit-lessonAttendance v-model="editModalShow" @save-success="getpage"></edit-lessonAttendance> -->
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
// import CreateLessonAttendance from "./create-lessonAttendance.vue";
// import EditLessonAttendance from "./edit-lessonAttendance.vue";

class PageLessonAttendanceRequest extends PageRequest {}

@Component({
  components: {}
})
export default class LessonAttendances extends AbpBase {
  pagerequest: PageLessonAttendanceRequest = new PageLessonAttendanceRequest();

  // createModalShow: boolean = false;
  // editModalShow: boolean = false;
  get list() {
    return this.$store.state.lessonAttendance.list;
  }
  get loading() {
    return this.$store.state.lessonAttendance.loading;
  }
  // create() {
  //   this.createModalShow = true;
  // }
  // edit(row) {
  //   this.$store.commit("lessonAttendance/edit", row);
  //   this.editModalShow = true;
  // }
  pageChange(page: number) {
    this.$store.commit("lessonAttendance/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("lessonAttendance/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "lessonAttendance/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.lessonAttendance.pageSize;
  }
  get totalCount() {
    return this.$store.state.lessonAttendance.totalCount;
  }
  get currentPage() {
    return this.$store.state.lessonAttendance.currentPage;
  }

  columns = [
    {
      title: this.L("Index"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
    },
    {
      title: this.L("StudentIndex"),
      key: "studentId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.order.student.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h: any, params: any) => {
        return h("span", params.row.order.student.name);
      }
    },
    {
      title: this.L("OrderIndex"),
      key: "orderId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.order.id).slice(-6));
      }
    },
    {
      title: this.L("LessonIndex"),
      key: "LessonId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.lesson.id).slice(-6));
      }
    },
    {
      title: this.L("ClassName"),
      key: "className",
      render: (h: any, params: any) => {
        return h("span", params.row.lesson.class.name);
      }
    },
    {
      title: this.L("LessonDate"),
      key: "lessonDate",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.lesson.lessonDate).toLocaleDateString()
        );
      }
    },
    {
      title: this.L("TimePeriod"),
      key: "timePeriod",
      render: (h, params) => {
        return h(
          "span",
          params.row.lesson.start.slice(0, 5) +
            " - " +
            params.row.lesson.end.slice(0, 5)
        );
      }
    },

    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.lesson.teacher.name);
      }
    },
    {
      title: this.L("Attended"),
      key: "attended",
      render: (h: any, params: any) => {
         return h("span", params.row.attended ? this.L("Yes") : this.L("No"));
      }
    }

    // {
    //   title: this.L("Actions"),
    //   key: "Actions",
    //   width: 250,
    //   slot: "action"
    // }
  ];
  async created() {
    this.getpage();
  }
}
</script>