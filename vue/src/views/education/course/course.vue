<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button
              icon="ios-search"
              type="primary"
              size="large"
              @click="getpage"
              class="toolbar-btn"
            >{{L('Refresh')}}</Button>
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
          <Page
            show-sizer
            class-name="fengpage"
            :total="totalCount"
            class="margin-top-10"
            @on-change="pageChange"
            @on-page-size-change="pagesizeChange"
            :page-size="pageSize"
            :current="currentPage"
          ></Page>
        </div>
      </div>
    </Card>
    <create-course v-model="createModalShow" @save-success="getpage"></create-course>
    <edit-course v-model="editModalShow" @save-success="getpage"></edit-course>
    <course-record v-model="recordModalShow"></course-record>
    <course-order v-model="orderModalShow"></course-order>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateCourse from "./create-course.vue";
import EditCourse from "./edit-course.vue";


class PageCourseRequest extends PageRequest {
  keyword: string = "";
  isActive: boolean = null;
}

@Component({
  components: { CreateCourse, EditCourse }
})
export default class Courses extends AbpBase {
  pagerequest: PageCourseRequest = new PageCourseRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  recordModalShow: boolean = false;
  orderModalShow: boolean = false;
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
  record() {
    this.recordModalShow = true;
  }
  order() {
    this.orderModalShow = true;
  }
  isActiveChange(val: string) {
    if (val === "Actived") {
      this.pagerequest.isActive = true;
    } else if (val === "NoActive") {
      this.pagerequest.isActive = false;
    } else {
      this.pagerequest.isActive = null;
    }
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
      title: this.L("Actions"),
      key: "Actions",
      width: 250,
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
  async created() {
    this.getpage();
  }
}
</script>