<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="8">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.keyword" :placeholder="L('StudentName')" />
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
            <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Students.Edit')||hasPermission('Pages.Records')||hasPermission('Pages.Orders')||hasPermission('Pages.Contracts')">
              <Button v-if="hasPermission('Pages.Students.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              <Button v-if="hasPermission('Pages.Orders')||hasPermission('Pages.Contracts')" type="primary" size="small" @click="order(row)" style="margin-right:5px">{{L('Order')}}</Button>
              <Button v-if="hasPermission('Pages.Lessons')" type="primary" size="small" @click="lesson(row)" style="margin-right:5px">{{L('Lessons')}}</Button>
            </template>
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <create-student v-model="createModalShow" @save-success="getpage"></create-student>
    <edit-student v-model="editModalShow" @save-success="getpage"></edit-student>
    <student-order v-model="orderModalShow"></student-order>
    <student-lesson v-model="lessonModalShow"></student-lesson>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "../../../store/entities/page-request";
import CreateStudent from "./create-student.vue";
import EditStudent from "./edit-student.vue";
import StudentOrder from "./student-order.vue";
import StudentLesson from "./student-lesson.vue";

class PageStudentRequest extends PageRequest {
  keyword: string = "";
}

@Component({
  components: { CreateStudent, EditStudent, StudentOrder, StudentLesson }
})
export default class Students extends AbpBase {
  pagerequest: PageStudentRequest = new PageStudentRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  orderModalShow: boolean = false;
  lessonModalShow: boolean = false;

  get list() {
    return this.$store.state.student.list;
  }
  get loading() {
    return this.$store.state.student.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("student/edit", row);
    this.editModalShow = true;
  }
  order(row) {
    this.$store.commit("student/edit", row);
    this.orderModalShow = true;
  }
  lesson(row) {
    this.$store.commit("student/edit", row);
    this.lessonModalShow = true;
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
      title: this.L("Index"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
    },
    {
      title: this.L("StudentName"),
      key: "name"
    },
    // {
    //   title: this.L("Province"),
    //   key: "province"
    // },
    {
      title: this.L("Location"),
      key: "Location",
      tooltip: true,
      render: (h: any, params: any) => {
        return h("span", [
          h("span", params.row.province),
          h("span", params.row.city),
          h("span", params.row.district),
          h("span", params.row.address)
        ]);
      }
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
    // {
    //   title: this.L("Relation"),
    //   key: "relation"
    // },
    {
      title: this.L("Phone"),
      key: "phone"
    },
    // {
    //   title: this.L("FatherPhone"),
    //   key: "fatherPhone"
    // },
    // {
    //   title: this.L("MotherPhone"),
    //   key: "motherPhone"
    // },
    {
      title: this.L("School"),
      key: "school"
    },
    {
      title: this.L("Birthday"),
      key: "birthday",
      render: (h: any, params: any) => {
        return h("span", params.row.birthday?new Date(params.row.birthday).toLocaleDateString():'');
      }
    },
    {
      title: this.L("LearnTeacher"),
      key: "leanTeacher",
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