<template>
  <div>
    <Modal :title="L('EditUser')" :value="value" @on-ok="save" @on-visible-change="visibleChange" :width='700'>
      <Form ref="userForm" label-position="top" :rules="userRule" :model="user">
        <Tabs value="detail">
          <TabPane :label="L('UserDetails')" name="detail">
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('UserName')" prop="userName">
                  <Input v-model="user.userName" :maxlength="32" :minlength="2" />
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('Name')" prop="name">
                  <Input v-model="user.name" :maxlength="32" />
                </FormItem>
              </i-col>
            </Row>

            <Row :gutter="16">
              <i-col span="8">
                <FormItem :label="L('UserType')" prop="type">
                  <Select v-model="user.type">
                    <Option v-for="item in userType" :key="item.key" :value="item.key">{{L(item.value)}}</Option>
                  </Select>
                </FormItem>
              </i-col>
              <i-col span="16">
                <FormItem :label="L('NativePlace')" prop="nativePlace">
                  <Cascader :data="data" v-model="nativePlace"></Cascader>
                </FormItem>
              </i-col>
            </Row>
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
          <TabPane :label="L('Else')" name="else">

            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('IdentityCard')" prop="identityCard">
                  <Input v-model="user.identityCard" />
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('PoliticsStatus')" prop="politicsStatus">
                  <Input v-model="user.politicsStatus" />
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('SchoolTag')" prop="schoolTag">
                  <Input v-model="user.schoolTag" />
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('EducationBackground')" prop="educationBackground">
                  <Input v-model="user.educationBackground" />
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('Nation')" prop="nation">
                  <Input v-model="user.nation" />
                </FormItem>
              </i-col>
              <i-col span="12">
                <FormItem :label="L('MaritalStatus')" prop="maritalStatus">
                  <Input v-model="user.maritalStatus" />
                </FormItem>
              </i-col>
            </Row>
            <Row :gutter="16">
              <i-col span="12">
                <FormItem :label="L('WorkingYears')" prop="workingYears">
                  <Input v-model="user.workingYears" />
                </FormItem>
              </i-col>
            </Row>
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
import Places from "../../../assets/json/city.json";
@Component
export default class EditUser extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  user: User = new User();
  userType = window.abp.custom.userType;
  data: any;

  get nativePlace() {
    return [this.user.province, this.user.city, this.user.district];
  }
  set nativePlace(val) {
    this.user.province = val.length > 0 ? val[0] : "";
    this.user.city = val.length > 1 ? val[1] : "";
    this.user.district = val.length > 2 ? val[2] : "";
  }

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
  created() {
    var temp = Places.map(m => {
      return {
        value: m.name,
        label: m.name,
        children: m.child.map(n => {
          return {
            value: n.name,
            label: n.name,
            children: n.child.map(o => {
              return { value: o.name, label: o.name };
            })
          };
        })
      };
    });

    this.data = temp;
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

