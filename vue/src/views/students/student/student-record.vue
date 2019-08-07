<template>
  <div>
    <Modal
      :title="L('RecordList')"
      :value="value"
      @on-visible-change="visibleChange"
      :mask-closable="false"
      width="1100px"
    >
      <div>
        <!-- <Card dis-hover> -->

        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <Col span="3">
              <h3>{{L('StudentName')}}:{{student.name}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('Phone')}}:{{student.phone}}</h3>
            </Col>
            <Col span="3">
              <h3>{{L('Father')}}:{{student.father}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('FatherPhone')}}:{{student.fatherPhone}}</h3>
            </Col>
            <Col span="3">
              <h3>{{L('Mother')}}:{{student.mother}}</h3>
            </Col>
            <Col span="5">
              <h3>{{L('MotherPhone')}}:{{student.motherPhone}}</h3>
            </Col>
          </Row>
          <hr
            style="border-width:1px 0 0 0; border-style:solid; border-top-color:#ccc; margin:10px 0"
          />
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Add')}}</Button>
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
        </div>
        <!-- </Card> -->
        <create-record v-model="createModalShow" @save-success="getpage"></create-record>
        <edit-record v-model="editModalShow" @save-success="getpage"></edit-record>
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
import Record from "../../../store/entities/record";
import CreateRecord from "../record/create-record.vue";
import EditRecord from "../record/edit-record.vue";
@Component({ components: { CreateRecord, EditRecord } })
export default class StudentRecord extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
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
  async getpage() {
    await this.$store.dispatch({
      type: "record/getRecordsByStudentId",
      data: { id: this.student.id }
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

  // category?: string
  // progess?: string
  // date?: string
  // content?: string
  // studentId?: string
  // studentName?: string
  // salesmanId?: number
  // salesmanName?: string
  // tenantId?: number
  // isDeleted?: boolean
  // deleterUserId?: number
  // deletionTime?: string
  // lastModificationTime?: string
  // lastModifierUserId?: number
  // creationTime?: string
  // creatorUserId?: number

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

