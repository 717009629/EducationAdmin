<template>
  <div>
    <!-- <Card dis-hover> -->

    <Form ref="queryForm" :label-width="100" label-position="left" inline>
      <!-- <hr style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0" /> -->
      <Row>
        <Button @click="create" icon="android-add" type="primary" >{{L('Add')}}</Button>
      </Row>
    </Form>
    <div class="margin-top-10">
      <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list"></Table>
    </div>
    <!-- </Card> -->
    <create-record v-model="createModalShow" @save-success="getpage"></create-record>
    <edit-record v-model="editModalShow" @save-success="getpage"></edit-record>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Record from "../../../store/entities/record";
import CreateRecord from "../record/create-record.vue";
import EditRecord from "../record/edit-record.vue";
@Component({ components: { CreateRecord, EditRecord } })
export default class StudentRecord extends AbpBase {
  @Prop({ type: Number, default: null }) studentId: null;
  student: Student = new Student();
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
  edit() {
    this.editModalShow = true;
  }

  @Watch("studentId")
  async watchId(nv, ov) {
    if(nv){
      await this.showPage()
    }
  }
  async getpage() {
    await this.$store.dispatch({
      type: "record/getAll",
      data: { studentId: this.student.id }
    });
  }

  async showPage() {
    this.student = Util.extend(true, {}, this.$store.state.student.editStudent);
    await this.getpage();
  }

  columns = [
    // {
    //   title: this.L("StudentName"),
    //   key: "name"
    // },

    {
      title: this.L("RecordContent"),
      key: "content"
    },
    {
      title: this.L("Progress"),
      key: "progress"
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
                  this.$store.commit("record/edit", params.row);
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
}
</script>

