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
              <Button v-if="hasPermission('Pages.Records.Create')" icon="ios-search" type="primary" size="large" @click="pageChange(1)" class="toolbar-btn">{{L('Find')}}</Button>
            </i-col>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Records.Edit')">
              <Button v-if="hasPermission('Pages.Records.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <edit-record v-model="editModalShow" @save-success="getpage"></edit-record>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditRecord from "./edit-record.vue";

class PageRecordRequest extends PageRequest {
  studentName: string = "";
}

@Component({
  components: { EditRecord }
})
export default class Records extends AbpBase {
  pagerequest: PageRecordRequest = new PageRecordRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.record.list;
  }
  get loading() {
    return this.$store.state.record.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("record/edit", row);
    this.editModalShow = true;
  }

  pageChange(page: number) {
    this.$store.commit("record/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("record/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "record/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.record.pageSize;
  }
  get totalCount() {
    return this.$store.state.record.totalCount;
  }
  get currentPage() {
    return this.$store.state.record.currentPage;
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
      title: this.L("CustomerIndex"),
      key: "CusotmerId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.customer.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h, params) => h("span", params.row.customer.studentName)
    },
    {
      title: this.L("RecordContent"),
      key: "content"
    },
    {
      title: this.L("State"),
      key: "state"
    },
    {
      title: this.L("RecordDate"),
      key: "date",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.date).toLocaleDateString());
      }
    },
    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      slot: "action"
    }
  ];
  async created() {
    this.$store.dispatch({
      type: "option/getAll",
      data: { maxResultCount: 10000, isActive: true }
    });
    this.getpage();
  }
}
</script>