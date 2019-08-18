<template>
  <div>
    <Modal :title="L('Lessone')" :value="value" @on-visible-change="visibleChange" :mask-closable="false" width="1200px">
      <div>
        <!-- <Card dis-hover> -->
        <div>
          <!-- <Card dis-hover> -->

          <Form ref="queryForm" :label-width="100" label-position="left" inline>
            <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
            <Row>
              <Button @click="create" icon="android-add" type="primary" v-if="hasPermission('Pages.Lessons.Create')">{{L('Add')}}</Button>
            </Row>
          </Form>
          <div class="margin-top-10">
            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
              <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Lessons.Edit')">
                <Button v-if="hasPermission('Pages.Lessons.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
              </template>
            </Table>
          </div>
          <!-- </Card> -->
          <create-lesson v-model="createModalShow" @save-success="getpage"></create-lesson>
          <edit-lesson v-model="editModalShow" @save-success="getpage"></edit-lesson>
        </div>
        <!-- </Card> -->
      </div>
      <div slot="footer"></div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import CreateLesson from "../lesson/create-lesson.vue";
import EditLesson from "../lesson/edit-lesson.vue";

@Component({ components: { CreateLesson, EditLesson } })
export default class StudentBusiness extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  student: Student = new Student();
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

  async getpage() {
    await this.$store.dispatch({
      type: "lesson/getAll",
      data: { studentId: this.student.id }
    });
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.student = Util.extend(
        true,
        {},
        this.$store.state.student.editStudent
      );
      this.getpage();
    }
  }
  columns = [
    // {
    //   title: this.L("Index"),
    //   key: "id",
    //   render: (h: any, params: any) => {
    //     return h("span", ("000000" + params.row.id).slice(-6));
    //   }
    // },

    {
      title: this.L("Course"),
      key: "Course"
    },
    {
      title: this.L("StudentName"),
      key: "studentName",
      render: (h: any, params: any) => {
        return h("span", params.row.student.name);
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
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 150,
      slot: "action"
    }
  ];
}
</script>

