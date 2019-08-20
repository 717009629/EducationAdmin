<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="6">
              <FormItem :label="L('Name')+':'" style="width:100%">
                <Input v-model="pagerequest.name" :placeholder="L('Name')" />
              </FormItem>
            </i-col>
          </Row>
          <Row>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" >
              <Button  type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('Lesson')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
     <teacher-lesson v-model="lessonModalShow"></teacher-lesson>
  </div>
  
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import TeacherLesson from "./teacher-lesson.vue";

class PageTeacherRequest extends PageRequest {
  name?: string;
}

@Component({
  components: { TeacherLesson}
})
export default class Teachers extends AbpBase {
  pagerequest: PageTeacherRequest = new PageTeacherRequest();

  lessonModalShow: boolean = false;
  get list() {
    return this.$store.state.teacher.list;
  }
  get loading() {
    return this.$store.state.teacher.loading;
  }
  lesson(row) {
    this.$store.commit("teacher/edit", row);
    this.lessonModalShow = true;
  }
  pageChange(page: number) {
    this.$store.commit("teacher/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("teacher/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "teacher/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.teacher.pageSize;
  }
  get totalCount() {
    return this.$store.state.teacher.totalCount;
  }
  get currentPage() {
    return this.$store.state.teacher.currentPage;
  }

  columns = [
    {
      title: this.L("UserName"),
      key: "userName"
    },
    {
      title: this.L("Name"),
      key: "name"
    },
    {
      title: this.L("Sex"),
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.sex
            ? this.L("Male")
            : params.row.sex === false
            ? this.L("Female")
            : ""
        );
      }
    },

    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("CreationTime"),
      key: "creationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.creationTime).toLocaleDateString()
        );
      }
    },
    {
      title: this.L("LastLoginTime"),
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.lastLoginTime).toLocaleString());
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      slot: "action"
    }
  ];
  async created() {
    this.getpage();
  }
}
</script>