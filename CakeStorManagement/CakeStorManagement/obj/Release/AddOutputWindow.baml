   M S B A M L   `   `   `  ���� M  ICakeStorManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CakeStorManagementCakeStorManagement  S.clr-namespace:CakeStorManagement.MyUserControl CakeStorManagement.MyUserControl  (    "CakeStorManagement.AddOutputWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:CakeStorManagement     V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  7uc.clr-namespace:CakeStorManagement.MyUserControl  -   $��addoutputwindow��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � Title$  Tạo phiếu xuất hàng��$	��300��$	��400��  AddOutputVM � DataContext# [ .���  � RowDefinitions �� $
��auto ��� .+�� $��0��.+�3   -CakeStorManagement.MyUserControl.ControlBarUC  
 � Tag �� *Title+	 �� ElementName$	 addoutputwindow��� $��1�� �� �� �� .+�(  "System.Windows.Controls.DatePicker  $��0��$��Bottom=�$	��150��)  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Ngày xuất�� ( $MaterialDesignFloatingHintDatePicker#��[    SelectedDateFormat$
 Long=�   SelectedDate �� *
DateOutput+�� $��1��$
 Người xuất�� �� 
IsEditable . $
��0 15q� �� DisplayMemberPath$ DisplayName��$��Center=�$	��Top=�$	��150�� & "MaterialDesignFloatingHintComboBox#��[ ���� *
UserList+ �� SelectedItem �� *SelectedUser+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��2�� $  MaterialDesignRaisedAccentButton#��[ $	��100��$��5q�$
��Xong��$	��Top=����� *FinishCommand+���� $	 addoutputwindow��