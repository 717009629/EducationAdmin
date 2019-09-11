<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.PrivateClasses.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list" :row-class-name="rowclassName">
            <template slot-scope="{ row }" slot="action">
              <Button type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('Lesson')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize"
                :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <class-lesson v-model="lessonModalShow"></class-lesson>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import classLesson from "./class-lesson.vue";

class PagePrivateClassRequest extends PageRequest {}

@Component({
  components: { classLesson }
})
export default class PrivateClasss extends AbpBase {
  pagerequest: PagePrivateClassRequest = new PagePrivateClassRequest();

  lessonModalShow: boolean = false;
  get list() {
    return this.$store.state.privateClass.list;
  }
  get loading() {
    return this.$store.state.privateClass.loading;
  }

  lesson(row) {
    this.$store.commit("class/edit", row.class);
    this.lessonModalShow = true;
  }

  pageChange(page: number) {
    this.$store.commit("privateClass/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("privateClass/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "privateClass/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.privateClass.pageSize;
  }
  get totalCount() {
    return this.$store.state.privateClass.totalCount;
  }
  get currentPage() {
    return this.$store.state.privateClass.currentPage;
  }
  rowclassName(row, index) {
    let c = [];
    if (row.state === 2) {
      c.push("success");
    }

    // if (row.course.privateClassType === 1) {
    //   c.push("font-error");
    // }
    return c;
  }
  columns = [
    {
      title: this.L("Index"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.class.id).slice(-6));
      }
    },
    {
      title: this.L("StudentIndex"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.student.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h: any, params: any) => {
        return h("span", params.row.student.name);
      }
    },
    {
      title: this.L("ClassName"),
      key: "className",
      render: (h: any, params: any) => {
        return h("span", params.row.class.name);
      }
    },
    {
      title: this.L("Course"),
      key: "course",
      render: (h: any, params: any) => {
        return h("span", params.row.course.name);
      }
    },

    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.class.teacher.name);
      }
    },
    {
      title: this.L("State"),
      key: "state",
      render: (h: any, params: any) => {
        return h(
          "span",
          this.L(window.abp.custom.ClassState[params.row.class.state])
        );
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
      title: this.L("Actions"),
      key: "Actions",
      width: 250,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
  }
}
</script>