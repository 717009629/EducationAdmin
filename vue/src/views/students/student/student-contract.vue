<template>
  <div>
    <!-- <Card dis-hover> -->
    <div class="margin-top-10">
      <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list"></Table>
    </div>
    <!-- </Card> -->
    <create-contract v-model="createModalShow" @save-success="getpage"></create-contract>
    <edit-contract v-model="editModalShow" @save-success="getpage"></edit-contract>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Contract from "../../../store/entities/contract";
import CreateContract from "../contract/create-contract.vue";
import EditContract from "../contract/edit-contract.vue";
@Component({ components: { CreateContract, EditContract } })
export default class StudentContract extends AbpBase {
  @Prop({ type: Number, default: null }) studentId: null;
  student: Student = new Student();
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

  @Watch("studentId")
  async watchId(nv, ov) {
    if (nv) {
      await this.showPage();
    }
  }
  async getpage() {
    await this.$store.dispatch({
      type: "contract/getAll",
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
      title: this.L("StartDate"),
      key: "startDate"
    },
    {
      title: this.L("EndDate"),
      key: "endDate"
    },
    {
      title: this.L("FullMoney"),
      key: "fullMoney"
    },
    {
      title: this.L("ContractState"),
      key: "state"
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
      title: this.L("ContractDate"),
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
}
</script>

