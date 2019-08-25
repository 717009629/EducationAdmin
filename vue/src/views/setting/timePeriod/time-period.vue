<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row>
            <Button v-if="hasPermission('Pages.TimePeriods.Create')" @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Refresh')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.TimePeriods.Edit')">
              <Button v-if="hasPermission('Pages.TimePeriods.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-timePeriod v-model="createModalShow" @save-success="getpage"></create-timePeriod>
    <edit-timePeriod v-model="editModalShow" @save-success="getpage"></edit-timePeriod>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateTimePeriod from "./create-time-period.vue";
import EditTimePeriod from "./edit-time-period.vue";

class PageTimePeriodRequest extends PageRequest {}

@Component({
  components: { CreateTimePeriod, EditTimePeriod }
})
export default class TimePeriods extends AbpBase {
  pagerequest: PageTimePeriodRequest = new PageTimePeriodRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.timePeriod.list;
  }
  get loading() {
    return this.$store.state.timePeriod.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("timePeriod/edit", row);
    this.editModalShow = true;
  }
  pageChange(page: number) {
    this.$store.commit("timePeriod/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("timePeriod/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "timePeriod/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.timePeriod.pageSize;
  }
  get totalCount() {
    return this.$store.state.timePeriod.totalCount;
  }
  get currentPage() {
    return this.$store.state.timePeriod.currentPage;
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
      title: this.L("Start"),
      key: "start"
    },
    {
      title: this.L("End"),
      key: "end"
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