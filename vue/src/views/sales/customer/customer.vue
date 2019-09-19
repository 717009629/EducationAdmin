<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="6">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.keyword" :placeholder="L('StudentName')+'/'+L('Phone')" />
              </FormItem>
            </i-col>
            <i-col span="6">
              <FormItem :label="L('State')+':'" style="width:100%">
                <Select v-model="pagerequest.state">
                  <Option value=""></Option>
                  <Option v-for="item  in recordStates" :key="item.id" :value="item.name" :label="item.name"></Option>
                </Select>
              </FormItem>
            </i-col>
          </Row>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Customers.Edit')||hasPermission('Pages.Records')">
              <Button v-if="hasPermission('Pages.Customers.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button v-if="hasPermission('Pages.Records')" type="primary" size="small" @click="record(row)" style="margin-right:5px">{{L('Record')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-customer v-model="createModalShow" @save-success="getpage"></create-customer>
    <edit-customer v-model="editModalShow" @save-success="getpage"></edit-customer>
    <customer-record v-model="recordModalShow"></customer-record>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "../../../store/entities/page-request";
import CreateCustomer from "./create-customer.vue";
import EditCustomer from "./edit-customer.vue";
import CustomerRecord from "./customer-record.vue";

class PageCustomerRequest extends PageRequest {
  keyword: string = "";
}

@Component({
  components: { CreateCustomer, EditCustomer, CustomerRecord }
})
export default class Customers extends AbpBase {
  pagerequest: PageCustomerRequest = new PageCustomerRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  recordModalShow: boolean = false;
  get list() {
    return this.$store.state.customer.list;
  }
  get loading() {
    return this.$store.state.customer.loading;
  }

  get recordStates() {
    return this.$store.state.option.list.filter(
      m => m.category === "recordState"
    );
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("customer/edit", row);
    this.editModalShow = true;
  }
  record(row) {
    this.$store.commit("customer/edit", row);
    this.recordModalShow = true;
  }
  pageChange(page: number) {
    this.$store.commit("customer/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("customer/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "customer/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.customer.pageSize;
  }
  get totalCount() {
    return this.$store.state.customer.totalCount;
  }
  get currentPage() {
    return this.$store.state.customer.currentPage;
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
      title: this.L("StudentName"),
      key: "studentName"
    },
    {
      title: this.L("Father"),
      key: "father"
    },
    {
      title: this.L("FatherPhone"),
      key: "fatherPhone"
    },
    {
      title: this.L("Mother"),
      key: "mother"
    },
    {
      title: this.L("MotherPhone"),
      key: "motherPhone"
    },
    {
      title: this.L("OtherGuadian"),
      key: "otherGuadian"
    },
    {
      title: this.L("OtherGuadianPhone"),
      key: "otherGuadianPhone"
    },

    {
      title: this.L("Note"),
      key: "note"
    },
    {
      title: this.L("State"),
      key: "state"
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
    this.$store.dispatch({
      type: "option/getAll",
      data: { maxResultCount: 10000, isActive: true }
    });
  }
}
</script>