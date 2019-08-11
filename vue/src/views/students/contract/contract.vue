<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.studentName" :placeholder="L('StudentName')" />
              </FormItem>
            </Col>
            <Col span="8">
              <Button
                icon="ios-search"
                type="primary"
                size="large"
                @click="getpage"
                class="toolbar-btn"
              >{{L('Find')}}</Button>
            </Col>
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
    <edit-contract v-model="editModalShow" @save-success="getpage"></edit-contract>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditContract from "./edit-contract.vue";
import ContractState from "../../../store/entities/contractState";

class PageContractRequest extends PageRequest {
  studentName: string = "";
}

@Component({
  components: { EditContract }
})
export default class Contracts extends AbpBase {
  pagerequest: PageContractRequest = new PageContractRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.contract.list;
  }
  get loading() {
    return this.$store.state.contract.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit() {
    this.editModalShow = true;
  }

  pageChange(page: number) {
    this.$store.commit("contract/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("contract/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "contract/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.contract.pageSize;
  }
  get totalCount() {
    return this.$store.state.contract.totalCount;
  }
  get currentPage() {
    return this.$store.state.contract.currentPage;
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
      key: "sutdentId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.student.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h, params) => h("span", params.row.student.name)
    },

    {
      title: this.L("StartDate"),
      key: "startDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.startDate).toLocaleDateString());
      }
    },
    {
      title: this.L("EndDate"),
      key: "endDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.endDate).toLocaleDateString());
      }
    },
    {
      title: this.L("FullMoney"),
      key: "fullMoney"
    },
    {
      title: this.L("ContractState"),
      key: "state",
      render: (h: any, params: any) => {
        return h("span", ContractState[params.row.state]);
      }
    },
    {
      title: this.L("Note"),
      key: "note"
    },
    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
    },
    {
      title: this.L("AuditorName"),
      key: "auditorName"
    },

    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
    },

    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
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
                  this.$store.commit("contract/edit", params.row);
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