﻿using Base.Assistant.Models;
using System.ComponentModel.DataAnnotations;

namespace Base.Models.Query.Identity;
public class UserDetailQuery(RequestInfo requestInfo) : IRequestInfo
{
    [Required(ErrorMessage = "لطفا مقدار نام کاربری را پر کنید")]
    public string? UserName { get; set; }
    [EmailAddress(ErrorMessage = "لطفا مقدار ایمیل را با فرمت درست پر کنید")]
    [Required(ErrorMessage = "لطفا مقدار ایمیل را پر کنید")]
    public string? Email { get; set; }
    public bool EmailConfimed { get; set; }
    [Required(ErrorMessage = "لطفا مقدار شماره تلفن را پر کنید")]
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfimed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public long AccessFaildCount { get; set; }
    public RequestInfo RequestInfo { get; private set; } = requestInfo;
}