<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.Subjects.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Subjects.Edit')">
              <Button v-if="hasPermission('Pages.Subjects.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template>
          </Table>
          <!-- <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page> -->
        </div>
      </div>
    </Card>
    <create-subject v-model="createModalShow" @save-success="getpage"></create-subject>
    <edit-subject v-model="editModalShow" @save-success="getpage"></edit-subject>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateSubject from "./create-subject.vue";
import EditSubject from "./edit-subject.vue";

//class PageSubjectRequest extends PageRequest {}

@Component({
  components: { CreateSubject, EditSubject }
})
export default class Subjects extends AbpBase {
  //pagerequest: PageSubjectRequest = new PageSubjectRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.subject.list;
  }
  get loading() {
    return this.$store.state.subject.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("subject/edit", row);
    this.editModalShow = true;
  }
  // pageChange(page: number) {
  //   this.$store.commit("subject/setCurrentPage", page);
  //   this.getpage();
  // }
  // pagesizeChange(pagesize: number) {
  //   this.$store.commit("subject/setPageSize", pagesize);
  //   this.getpage();
  // }
  async getpage() {
    // this.pagerequest.maxResultCount = this.pageSize;
    // this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "subject/getAll",
      data: {maxResultCount:1000}
    });
  }
  get pageSize() {
    return this.$store.state.subject.pageSize;
  }
  get totalCount() {
    return this.$store.state.subject.totalCount;
  }
  get currentPage() {
    return this.$store.state.subject.currentPage;
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
      title: this.L("SubjectName"),
      key: "name"
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
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