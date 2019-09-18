<template>
  <div>
    <Modal :title="L('ChangePassword')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :width='400'>
      <Form ref="changPasswordForm" label-position="top" :rules="changePasswordRule" :model="changePasswordByAdmin">
        <FormItem :label="L('Password')" prop="password">
          <Input v-model="changePasswordByAdmin.password" :maxlength="32" />
        </FormItem>
        <FormItem :label="L('ConfirmPassword')" prop="confirmPassword">
          <Input v-model="changePasswordByAdmin.confirmPassword" :maxlength="32" />
        </FormItem>
      </Form>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="save" type="primary">{{L('OK')}}</Button>
      </div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import ChangepasswordByAdmin from "../../../store/entities/changePasswordByAdmin";
import Places from "../../../assets/json/city.json";
@Component
export default class ChangePassword extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  // user: User = new User();
  changePasswordByAdmin: ChangepasswordByAdmin = new ChangepasswordByAdmin();

  save() {
    (this.$refs.changPasswordForm as any).validate(async (valid: boolean) => {
      if (valid) {
        this.changePasswordByAdmin.id = this.$store.state.user.editUser.id;
        await this.$store.dispatch({
          type: "user/changePasswordByAdmin",
          data: this.changePasswordByAdmin
        });
        (this.$refs.changPasswordForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.changPasswordForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
      this.changePasswordByAdmin.password = "";
      this.changePasswordByAdmin.confirmPassword = "";
    } else {
      // this.changePasswordByAdmin = new ChangepasswordByAdmin();
      //this.changePasswordByAdmin.id = this.$store.state.user.editUser.id;
      // this.changePasswordByAdmin.password = "";
      // this.changePasswordByAdmin.confirmPassword = "";
      //(this.$refs.changPasswordForm as any).resetFields();
      //console.log(this.changePasswordByAdmin);
    }
  }
  validatePassCheck2 = (rule: any, value: any, callback: any) => {
    console.log(this.changePasswordByAdmin);

    if (!value) {
      callback(new Error(this.L("FieldIsRequired")));
    } else if (value !== this.changePasswordByAdmin.password) {
      callback(new Error(this.L("ConfirmPasswordNotMatch")));
    } else {
      callback();
    }
    console.log(value);
  };
  changePasswordRule = {
    password: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Password")),
        trigger: "blur"
      }
    ],
    confirmPassword: { validator: this.validatePassCheck2, trigger: "blur" }
  };
}
</script>

