<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <i-col span="8">
              <FormItem :label="L('Keyword')+':'" style="width:100%">
                <Input v-model="pagerequest.className" :placeholder="L('ClassName')" />
              </FormItem>
            </i-col>
            <i-col span="8">
              <Button v-if="hasPermission('Pages.Lessons.Create')" icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">{{L('Find')}}</Button>
            </i-col>
          </Row>
        </Form>
        <div class="margin-top-10">
          <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
            <!-- <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Lessons.Edit')">
              <Button v-if="hasPermission('Pages.Lessons.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
            </template> -->
          </Table>
          <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage">
          </Page>
        </div>
      </div>
    </Card>
    <edit-lesson v-model="editModalShow" @save-success="getpage"></edit-lesson>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditLesson from "./edit-lesson.vue";

class PageLessonRequest extends PageRequest {
  className: string = "";
}

@Component({
  components: { EditLesson }
})
export default class Lessons extends AbpBase {
  pagerequest: PageLessonRequest = new PageLessonRequest();

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.lesson.list;
  }
  get loading() {
    return this.$store.state.lesson.loading;
  }
  create() {
    this.createModalShow = true;
  }
  edit(row) {
    this.$store.commit("lesson/edit", row);
    this.editModalShow = true;
  }

  pageChange(page: number) {
    this.$store.commit("lesson/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("lesson/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;

    await this.$store.dispatch({
      type: "lesson/getAll",
      data: this.pagerequest
    });
  }
  get pageSize() {
    return this.$store.state.lesson.pageSize;
  }
  get totalCount() {
    return this.$store.state.lesson.totalCount;
  }
  get currentPage() {
    return this.$store.state.lesson.currentPage;
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
      title: this.L("Course"),
      key: "course"
    },
    {
      title: this.L("ClassIndex"),
      key: "classId",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.classId).slice(-6));
      }
    },
    {
      title: this.L("ClassName"),
      key: "className",
      render: (h: any, params: any) => {
        return h("span", params.row.class.name);
      }
    },
    {
      title: this.L("LessonDate"),
      key: "lessonDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.lessonDate).toLocaleDateString());
      }
    },
    {
      title: this.L("LessonNumber"),
      key: "lessonNumber"
    },
    {
      title: this.L("LessonDate"),
      key: "lessonDate",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.lessonDate).toLocaleDateString());
      }
    },

    {
      title: this.L("Teacher"),
      key: "teacher",
      render: (h: any, params: any) => {
        return h("span", params.row.teacher.name);
      }
    },
    {
      title: this.L("IsFinish"),
      key: "isFinish",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.isFinish ? this.L("Fininshed") : this.L("Unfinished")
        );
      }
    }
    // {
    //   title: this.L("Actions"),
    //   key: "Actions",
    //   width: 150,
    //   slot: "action"
    // }
  ];
  async created() {
    this.getpage();
  }
}
</script>