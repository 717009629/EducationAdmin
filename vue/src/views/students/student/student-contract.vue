<template>
  <div>
    <!-- <Card dis-hover> -->
    <div class="margin-top-10">
      <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
        <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Contracts.Edit')||hasPermission('Pages.Contracts.Audite')">
          <Button v-if="canEdit(row)" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
          <Button v-if="canAudite(row)" type="primary" size="small" @click="audite(row)" style="margin-right:5px">{{L('Audite')}}</Button>
        </template>
      </Table>
    </div>
    <!-- </Card> -->
    <edit-contract v-model="editModalShow" @save-success="getpage"></edit-contract>
    <audite-contract v-model="auditeModalShow" @save-success="getpage"></audite-contract>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Student from "../../../store/entities/student";
import Contract from "../../../store/entities/contract";
import EditContract from "../../sales/contract/edit-contract.vue";
import ContractState from "../../../store/entities/contractState";
import AuditeContract from "../../sales/contract/audite-contract.vue";
@Component({ components: { EditContract, AuditeContract } })
export default class StudentContract extends AbpBase {
  @Prop({ type: Number, default: null }) studentId: null;
  student: Student = new Student();
  editModalShow: boolean = false;
  auditeModalShow: boolean = false;

  canEdit(contract: Contract) {
    return (
      this.hasPermission("Pages.Contracts.Edit") &&
      (contract.state === ContractState.Created ||
        contract.state === ContractState.WaitAudite ||
        contract.state === ContractState.Reject)
    );
  }
  canAudite(contract: Contract) {
    return (
      this.hasPermission("Pages.Contracts.Audite") &&
      (contract.state === ContractState.Created ||
        contract.state === ContractState.WaitAudite)
    );
  }

  get list() {
    return this.$store.state.contract.list;
  }
  get loading() {
    return this.$store.state.contract.loading;
  }
  edit(row) {
    this.$store.commit("contract/edit", row);
    this.editModalShow = true;
  }
  audite(row) {
    this.$store.commit("contract/edit", row);
    this.auditeModalShow = true;
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
    {
      title: this.L("Index"),
      key: "id",
      render: (h: any, params: any) => {
        return h("span", ("000000" + params.row.id).slice(-6));
      }
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
      title: this.L("AuditedReason"),
      key: "auditedReason",
      tooltip: true
    },
    {
      title: this.L("Note"),
      key: "note",
      tooltip: true
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
      slot: "action"
    }
  ];
}
</script>

