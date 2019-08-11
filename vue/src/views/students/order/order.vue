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
    <edit-order v-model="editModalShow" @save-success="getpage"></edit-order>
    <order-course v-model="courseModalShow"></order-course>
    <create-contract v-model="contractModalShow" ></create-contract>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditOrder from "./edit-order.vue";
import OrderCourse from "./order-course.vue";
import CreateContract from "../contract/create-contract.vue";

class PageOrderRequest extends PageRequest {
  studentName: string = "";
}

@Component({
  components: { EditOrder, OrderCourse, CreateContract }
})
export default class Orders extends AbpBase {
  pagerequest: PageOrderRequest = new PageOrderRequest();

  editModalShow: boolean = false;
  courseModalShow: boolean = false;
  contractModalShow: boolean = false;
  get list() {
    return this.$store.state.order.list;
  }
  get loading() {
    return this.$store.state.order.loading;
  }
  edit() {
    this.editModalShow = true;
  }
  showCourse() {
    this.courseModalShow = true;
  }
  showContract() {
    this.contractModalShow = true;
  }

  pageChange(page: number) {
    this.$store.commit("order/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("order/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "order/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.order.pageSize;
  }
  get totalCount() {
    return this.$store.state.order.totalCount;
  }
  get currentPage() {
    return this.$store.state.order.currentPage;
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
      title: this.L("OrderDate"),
      key: "orderDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.orderDate).toLocaleDateString());
      }
    },
    {
      title: this.L("SchoolBegin"),
      key: "schoolBegin",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.schoolBegin).toLocaleDateString());
      }
    },
    {
      title: this.L("FullMomeny"),
      key: "fullMomeny"
    },
    {
      title: this.L("ClassName"),
      key: "className"
    },
    {
      title: this.L("State"),
      key: "state"
    },
    {
      title: this.L("SalesmanName"),
      key: "salesmanName"
    },

    {
      title: this.L("Actions"),
      key: "Actions",
      width: 220,
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
                  this.$store.commit("order/edit", params.row);
                  this.edit();
                }
              }
            },
            this.L("Edit")
          ),
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
                  this.$store.commit("order/edit", params.row);
                  this.showCourse();
                }
              }
            },
            this.L("Course")
          ),
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
                  this.$store.commit("order/edit", params.row);
                  this.showContract();
                }
              }
            },
            this.L("Contract")
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