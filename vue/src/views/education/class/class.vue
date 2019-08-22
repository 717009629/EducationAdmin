<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.Classes.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Classes.Edit')">
              <Button v-if="hasPermission('Pages.Classes.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('Lesson')}}</Button>
              <Button type="primary" size="small" @click="order(row)" style="margin-right:5px">{{L('Student')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-class v-model="createModalShow" @save-success="getpage"></create-class>
    <edit-class v-model="editModalShow" @save-success="getpage"></edit-class>
    <class-lesson v-model="lessonModalShow"></class-lesson>
     <class-order v-model="orderModalShow"></class-order>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateClass from "./create-class.vue";
import EditClass from "./edit-class.vue";
import ClassLesson from "./class-lesson.vue";
import ClassOrder from "./class-order.vue";

class PageClassRequest extends PageRequest {}

@Component({
  components: { CreateClass, EditClass, ClassLesson ,ClassOrder}
})
export default class Classs extends AbpBase {
  pagerequest: PageClassRequest = new PageClassRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  lessonModalShow: boolean = false;
  orderModalShow: boolean = false;
  get list() {
    return this.$store.state.class.list;
  }
  get loading() {
    return this.$store.state.class.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("class/edit", row);
    this.editModalShow = true;
  }
  lesson(row) {
    this.$store.commit("class/edit", row);
    this.lessonModalShow = true;
  }
  order(row) {
    this.$store.commit("class/edit", row);
    this.orderModalShow = true;
  }
  pageChange(page: number) {
    this.$store.commit("class/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("class/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "class/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.class.pageSize;
  }
  get totalCount() {
    return this.$store.state.class.totalCount;
  }
  get currentPage() {
    return this.$store.state.class.currentPage;
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
      title: this.L("ClassName"),
      key: "name"
    },
    {
      title: this.L("Course"),
      key: "course",
      render: (h: any, params: any) => {
        return h("span", params.row.course.name);
      }
    },

    {
      title: this.L("ClassType"),
      key: "classType",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.course.classType === 0
            ? this.L("OneToMany")
            : params.row.course.classType === 1
            ? this.L("OneToOne")
            : ""
        );
      }
    },
    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.teacher.name);
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