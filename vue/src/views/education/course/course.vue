<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.Courses.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Courses.Edit')">
              <Button v-if="hasPermission('Pages.Courses.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-course v-model="createModalShow" @save-success="getpage"></create-course>
    <edit-course v-model="editModalShow" @save-success="getpage"></edit-course>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateCourse from "./create-course.vue";
import EditCourse from "./edit-course.vue";

class PageCourseRequest extends PageRequest {}

@Component({
  components: { CreateCourse, EditCourse }
})
export default class Courses extends AbpBase {
  pagerequest: PageCourseRequest = new PageCourseRequest();

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
  edit(row) {
    this.$store.commit("course/edit", row);
    this.editModalShow = true;
  }
  pageChange(page: number) {
    this.$store.commit("course/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("course/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "course/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.course.pageSize;
  }
  get totalCount() {
    return this.$store.state.course.totalCount;
  }
  get currentPage() {
    return this.$store.state.course.currentPage;
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
      title: this.L("ClassType"),
      key: "classType",
      render: (h, params) => {
        return h("span", this.L(window.abp.custom.ClassType[params.row.classType]));
      }
    },
    {
      title: this.L("Category"),
      key: "category"
    },
    {
      title: this.L("CourseName"),
      key: "name"
    },
    {
      title: this.L("Price"),
      key: "price"
    },
    {
      title: this.L("Note"),
      key: "note"
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