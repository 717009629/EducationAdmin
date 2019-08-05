<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.keyword" :placeholder="L('StudentName')"></Input>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
            <Button
              icon="ios-search"
              type="primary"
              size="large"
              @click="getpage"
              class="toolbar-btn"
            >{{L('Find')}}</Button>
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
    <create-student v-model="createModalShow" @save-success="getpage"></create-student>
    <edit-student v-model="editModalShow" @save-success="getpage"></edit-student>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateStudent from "./create-student.vue";
import EditStudent from "./edit-student.vue";

class PageStudentRequest extends PageRequest {
  keyword: string = "";
  isActive: boolean = null;
}

@Component({
  components: { CreateStudent, EditStudent }
})
export default class Students extends AbpBase {
  edit() {
    this.editModalShow = true;
  }

  pagerequest: PageStudentRequest = new PageStudentRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.student.list;
  }
  get loading() {
    return this.$store.state.student.loading;
  }
  create() {
    this.createModalShow = true;
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
    this.$store.commit("student/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("student/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "student/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.student.pageSize;
  }
  get totalCount() {
    return this.$store.state.student.totalCount;
  }
  get currentPage() {
    return this.$store.state.student.currentPage;
  }

  columns = [
    {
      title: this.L("StudentName"),
      key: "name"
    },
    {
      title: this.L("Province"),
      key: "province"
    },
    {
      title: this.L("City"),
      key: "city"
    },
    {
      title: this.L("Address"),
      key: "address"
    },
    {
      title: this.L("Sex"),
      render: (h: any, params: any) => {
        return h("span", params.row.sex ? this.L("Male") :params.row.sex===false? this.L("Female"):'');
      }
    },
    {
      title: this.L("Parent"),
      key: "parent"
    },
    {
      title: this.L("Relation"),
      key: "relation"
    },
    {
      title: this.L("Phone"),
      key: "phone"
    },
    {
      title: this.L("FatherPhone"),
      key: "fatherPhone"
    },
    {
      title: this.L("MotherPhone"),
      key: "motherPhone"
    },
    {
      title: this.L("School"),
      key: "school"
    },
    {
      title: this.L("Birthday"),
      key: "birthday"
    },
    {
      title: this.L("Grade"),
      key: "grade"
    },
    {
      title: this.L("CourseType"),
      key: "courseType"
    },
    {
      title: this.L("StudentType"),
      key: "studentType"
    },
    {
      title: this.L("TeachMethod"),
      key: "teachMethod"
    },
    {
      title: this.L("Origin"),
      key: "origin"
    },
    {
      title: this.L("Salesman"),
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
                  this.$store.commit("student/edit", params.row);
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
                type: "error",
                size: "small"
              },
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Tips"),
                    content: this.L("DeleteStudentConfirm"),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "student/delete",
                        data: params.row
                      });
                      await this.getpage();
                    }
                  });
                }
              }
            },
            this.L("Delete")
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