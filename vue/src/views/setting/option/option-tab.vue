<template>
  <div class="margin-top-10">
    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
      <template slot-scope="{ row }" slot="action" v-if="hasPermission('Pages.Options.Edit')">
        <Button v-if="hasPermission('Pages.Options.Edit')" type="primary" size="small" @click="edit(row)" style="margin-right:5px">{{L('Edit')}}</Button>
      </template>
    </Table>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import EditOption from "./edit-option.vue";

class PageOptionRequest extends PageRequest {}

@Component({
  components: { EditOption }
})
export default class OptionTab extends AbpBase {
  @Prop({ type: String }) category: string;
  pagerequest: PageOptionRequest = new PageOptionRequest();
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.option.list.filter(
      m => m.catetory === this.category
    );
  }
  get loading() {
    return this.$store.state.option.loading;
  }

  edit(row) {
    this.$store.commit("option/edit", row);
    this.editModalShow = true;
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
      title: this.L("OptionName"),
      key: "name"
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      }
    },
    {
      title: this.L("Actions"),
      key: "Actions",
      width: 250,
      slot: "action"
    }
  ];
}
</script>