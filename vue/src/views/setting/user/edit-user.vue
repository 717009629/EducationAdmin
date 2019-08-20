<template>
  <div>
    <Modal :title="L('EditUser')" :value="value" @on-ok="save" @on-visible-change="visibleChange">
      <Form ref="userForm" label-position="top" :rules="userRule" :model="user">
        <Tabs value="detail">
          <TabPane :label="L('UserDetails')" name="detail">
            <FormItem :label="L('UserName')" prop="userName">
              <Input v-model="user.userName" :maxlength="32" :minlength="2" />
            </FormItem>
            <FormItem :label="L('Name')" prop="name">
              <Input v-model="user.name" :maxlength="32" />
            </FormItem>
            <FormItem :label="L('UserType')" prop="type">
              <Select v-model="user.type">
                <Option :value="0">{{L("Business")}}</Option>
                <Option :value="1">{{L("Teacher")}}</Option>
                <Option :value="2">{{L("Admin")}}</Option>
              </Select>
            </FormItem>
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('Sex')" prop="sex">
                  <RadioGroup v-model="sex" type="button">
                    <Radio label="male">{{L('Male')}}</Radio>
                    <Radio label="female">{{L('Female')}}</Radio>
                  </RadioGroup>
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('Birthday')" prop="birthday">
                  <DatePicker type="date" placeholder="Select date" v-model="user.birthday"></DatePicker>
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('EmailAddress')" prop="emailAddress">
                  <Input v-model="user.emailAddress" type="email" :maxlength="32" />
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('Phone')" prop="phone">
                  <Input v-model="user.Phone" type="tel" />
                </FormItem>
              </i-col>
            </Row>

            <FormItem>
              <Checkbox v-model="user.isActive">{{L('IsActive')}}</Checkbox>
            </FormItem>
          </TabPane>
          <TabPane :label="L('UserRoles')" name="roles">
            <CheckboxGroup v-model="user.roleNames">
              <Checkbox :label="role.normalizedName" v-for="role in roles" :key="role.id"><span>{{role.name}}</span></Checkbox>
            </CheckboxGroup>
          </TabPane>
        </Tabs>
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
import User from "../../../store/entities/user";
@Component
export default class EditUser extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  user: User = new User();
  created() {}
    get sex() {
    return this.user.sex ? "male" : this.user.sex === false ? "female" : "";
  }
  set sex(val) {
    this.user.sex = val === "male" ? true : val === "female" ? false : null;
  }
  get roles() {
    return this.$store.state.user.roles;
  }
  save() {
    (this.$refs.userForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "user/update",
          data: this.user
        });
        (this.$refs.userForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.userForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    } else {
      this.user = Util.extend(true, {}, this.$store.state.user.editUser);
    }
  }
  userRule = {
    userName: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("UserName")),
        trigger: "blur"
      }
    ],
    name: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Name")),
        trigger: "blur"
      }
    ],

    emailAddress: [
      {
        required: true,
        message: this.L("FieldIsRequired", undefined, this.L("Email")),
        trigger: "blur"
      },
      { type: "email" }
    ]
  };
}
</script>

